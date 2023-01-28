using SpamTools.Data;

if(args == null || !args.Any())
{
    Console.WriteLine("No filename specified.");
}

var emailParser = new EmailParser(args[0]);
var ips = emailParser.GetRecievedIpAddreesses().Distinct();
var urls = emailParser.GetUrls().Distinct();

File.WriteAllLines("RecievedIPs.txt", ips);
File.WriteAllLines("ULRs.txt", urls);

Console.WriteLine($"{Environment.NewLine}There where {ips.Count()} IP Addrresses.");
Console.WriteLine($"There where {urls.Count()} URLs.");