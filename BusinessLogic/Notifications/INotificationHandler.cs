namespace BusinessLogic.Notifications
{
    public interface INotificationHandler<Notification> 
        where Notification : class
    {
        public void Handle(Notification notification);
    }
}
