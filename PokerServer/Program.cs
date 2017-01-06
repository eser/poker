
namespace PokerServer {
	public static class Program {
		public static void Main() {
			// Console.WriteLine("This program requires to be installed as an operating system service.");

			Server _server = new Server();
			_server.Start();
		}
	}
}

