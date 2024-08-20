namespace FileDownload;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        var peer = new Peer();
        var task = peer.Start(cancellationToken);

        if (args.Length > 0 && args[0] == "download")
        {
            if (args.Length >= 5)
            {
                await peer.DownloadFile(args[1], Convert.ToInt32(args[2]), args[3], args[4], cancellationToken);
            }
            else
            {
                Console.WriteLine("Uso incorrecto de argumentos. Se requiere: download <peerIP> <peerPort> <fileName> <savePath>");
            }
        }
        else
        {
            Console.WriteLine("waiting for other peers to connect...");
        }

        await task;
    }
}
