using HtmlAgilityPack;
using System;
using System.IO;

namespace AWSAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(ReadText());

            var links = doc.DocumentNode.SelectNodes("//a");

            foreach(var link in links)
            {
                var tags = "";

                foreach(var attr in link.Attributes)
                {
                    if (attr.Name.Equals("href"))
                        continue;

                    tags += $"{attr.Name}:{attr.Value};";
                }

                link.Attributes.Add("ses:tags", tags);
            }

            var write = new StringWriter();

            doc.Save(write);

            Console.WriteLine(write.GetStringBuilder().ToString());
        }

        static string ReadText()
        {
            var path = @"D:\Workspaces\.Net\Replace-Attribute-Playground\index.html";

            return File.ReadAllText(path);
        }
    }
}
