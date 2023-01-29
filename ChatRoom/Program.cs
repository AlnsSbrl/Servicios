namespace ChatRoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Servidor s = new Servidor();
            Thread chat = new Thread(s.IniciarChatRoom);
            Thread anotherChat = new Thread(s.IniciarChatRoom);
            //Servidor r = new Servidor(3);
            ///Thread otroChat = new Thread(r.IniciarChatRoom);//por algun motivo necesito OTRO objeto servidor
            chat.Start();
            anotherChat.Start();
            //otroChat.Start(); //SUPONGO que este "petaria", pero usaria el puerto de backup que le puse?
        }
    }
}