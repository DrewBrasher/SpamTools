# Spam Tools

This is a C# tool for extracting data from spam emails.

I plan to add more features, but currently it is a class library that extracts URLs and Received IPs from an email file like .mbox. It has a console app that saves the data to text files.

## How to use
Download the executable file for your operating system in the /dist folder of this repository and run this from the command line in the folder you downloaded it to:
`SpamTools.Console.exe {path to email file}`
for example, if you have an mbox file called `SomeSpamEmails.mbox` in the same directory as `SpamTools.Console.exe`, you would run this from the command line:
`SpamTools.Console.exe SomeSpamEmails.mbox`
It will generate 2 textfiles in the same directory that contain the URLs and Received IP Addresses.
