namespace NetCoreApp.Models.Service
{
	public interface IRabbitService
	{
		void Send(byte[] file_bytes);

		void Receive();
	}
}