using System;
using System.Text.RegularExpressions;

namespace SpamTools.Data;

public class EmailParser
{
    public IList<string> FileLines { get; set; } = new List<string>();

    public EmailParser(string emailFilePath)
	{
        FileLines = File.ReadAllLines(emailFilePath);
    }

	public IEnumerable<string> GetRecievedIpAddreesses()
    {
        if (!FileLines.Any()) { return new List<string>(); }

        // regex source: https://ihateregex.io/expr/ip/
        var ip4Regex = new Regex(@"(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}");
        // regex source: https://ihateregex.io/expr/ipv6/
        //var ip6Regex = new Regex(@"(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))");

        var receivedIps = FileLines.Where(el => el.ToLower().StartsWith("received:"))
			.Select(el => ip4Regex.Match(el).Value)
			.Where(ip => !string.IsNullOrWhiteSpace(ip));

		return receivedIps;
    }

    public IEnumerable<string> GetUrls()
    {
        if (!FileLines.Any()) { return new List<string>(); }

        // regex source: https://ihateregex.io/expr/url/
        var urlRegex = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()!@:%_\+.~#?&\/\/=]*)");
        
        var urls = FileLines
            .SelectMany(el => urlRegex.Matches(el).Select(url => url.Value))
            .Where(url => !string.IsNullOrWhiteSpace(url));

        return urls;
    }
}

