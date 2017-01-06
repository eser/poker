using System;
using System.ServiceProcess;

namespace PokerServer {
	public class Service : ServiceBase {
		private Server m_Server;

		public Service() {
			this.ServiceName = "PokerServer";
		}

		protected override void OnStart(string[] args) {
			base.OnStart(args);

			this.m_Server = new Server();
			this.m_Server.Start();
		}

		protected override void OnStop() {
			this.m_Server.Dispose();
			this.m_Server = null;

			base.OnStop();
		}
	}
}

