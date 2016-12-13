# Log2Console
GitHub clone of SVN repo https://log2console.svn.codeplex.com/svn (cloned by http://svn2github.com/)

It appeared to me that the project on Codeplex was no longer being maintained, so I forked to project to make some fixes that were bothering me.

Changes from Codeplex version
--
* TCP Receiver will attempt to parse multiple messages received over the same socket connection.
* UDP Reciever, added support for Serilog messages in addition to Log4J/NLog.
