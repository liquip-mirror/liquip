using System;
using System.IO.Compression;
using System.Net.Sockets;
using System.Security.Cryptography;
using PlugMaker;

new Random();
new TcpClient();
MD5.Create();

await MD5.Create().ComputeHashAsync(new MemoryStream());

ClassBuilder.Add(LibraryImportFinder.Find());
ClassBuilder.Build();
