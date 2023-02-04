namespace ChatRoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Servidor s = new Servidor();
            Thread chat = new Thread(s.IniciarChatRoom);
            Thread anotherChat = new Thread(s.IniciarChatRoom);
            Thread chitChat = new Thread(s.IniciarChatRoom);
            chat.Start();
            anotherChat.Start();
            chitChat.Start();          
        }
    }
}