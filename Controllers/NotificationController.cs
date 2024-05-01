using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class NotificationController : ControllerBase
{
    [HttpPost("notification")]
    public async Task<IActionResult> PostNotification([FromBody] MessageDto message)
    {
        try
        {
            var messageNot = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = message.Notification.Title,
                    Body = message.Notification.Body,
                    ImageUrl = message.Notification.Icon
                },
                Token = message.To
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(messageNot);
            if (!string.IsNullOrEmpty(result))
            {
                // Message was sent successfully
                return Ok("Notificacao enviada");
            }
            else
            {
                // There was an error sending the message
                return BadRequest("Erro para enviar a notificação");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}