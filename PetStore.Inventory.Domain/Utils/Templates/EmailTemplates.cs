using PetStore.Inventory.Domain.Utils.Enums;

namespace PetStore.Inventory.Domain.Utils.Templates
{

    public static class EmailTemplates
    {
        public static string GetTemplate(EEmailType type, string token)
        {
            return type switch
            {
                EEmailType.Welcome => WelcomeTemplate(token), // (...)
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        private static string WelcomeTemplate(string token)
        {
            string mailObj =
                $@"<!DOCTYPE html>
                   <html lang=""pt-BR"">
                   <head>
                     <meta charset=""UTF-8"">
                     <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                     <title>Boas-vindas ao Pet Store 🐾</title>
                   </head>
                   
                   <body style=""margin:0; padding:0; background-color:#f4f7f8; font-family:'Segoe UI',Arial,sans-serif;"">
                   
                     <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""padding:20px;"">
                       <tr>
                         <td align=""center"">
                   
                           <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background:white; border-radius:16px; overflow:hidden; box-shadow:0 8px 30px rgba (0,0,0,0.08);"">
                   
                             <!-- HEADER com ilustração -->
                             <tr>
                               <td style=""background:linear-gradient(135deg,#2EC4B6 0%,#1ea89a 100%); padding:40px 30px; text-align:center; color:white;"">
                                 <div style=""font-size:48px; margin-bottom:8px;"">🐾</div>
                                 <h1 style=""margin:0; font-size:28px; font-weight:700; letter-spacing:-0.5px;"">Pet Store</h1>
                                 <p style=""margin:6px 0 0; font-size:16px; opacity:0.9; font-weight:300;"">O cuidado que seu pet merece</p>
                               </td>
                             </tr>
                   
                             <!-- BODY -->
                             <tr>
                               <td style=""padding:35px 40px; color:#2d3748;"">
                   
                                 <h2 style=""margin:0 0 8px; font-size:22px; color:#1a202c;"">Olá, seja bem-vindo! 🎉</h2>
                                 <p style=""margin:0 0 20px; font-size:15px; color:#4a5568; line-height:1.6;"">
                                   Que alegria ter você conosco! Sua conta no <strong>Pet Store</strong> foi criada com sucesso.
                                   Agora você faz parte da nossa família de apaixonados por pets.
                                 </p>
                   
                                 <div style=""background:#f0faf9; border-radius:12px; padding:20px 24px; margin:25px 0; border:1px solid #d4ece8;"">
                                   <p style=""margin:0 0 6px; font-size:13px; color:#2d6b63; text-transform:uppercase; letter-spacing:0.5px; font-weight:600;"">
                                     🔑 Token de acesso único
                                   </p>
                                   <p style=""margin:0; font-size:36px; font-weight:700; color:#1ea89a; letter-spacing:4px; word-break:break-all;"">
                                     {token}
                                   </p>
                                   <p style=""margin:8px 0 0; font-size:13px; color:#4a7a72;"">
                                     Use este token para acessar sua conta pela primeira vez.
                                   </p>
                                 </div>
                   
                                 <div style=""background:#f7fafc; border-radius:10px; padding:16px 20px; margin:20px 0 25px;"">
                                   <p style=""margin:0; font-size:14px; color:#4a5568; line-height:1.5;"">
                                     💡 <strong>Dica:</strong> Recomendamos alterar seu token após o primeiro acesso por segurança.
                                   </p>
                                 </div>
                   
                                 <p style=""font-size:13px; color:#718096; line-height:1.5; margin:0;"">
                                   ⚠️ Este e-mail foi gerado automaticamente. Se você não solicitou esta conta,
                                   <strong style=""color:#2d3748;"">ignore esta mensagem</strong> ou entre em contato conosco.
                                 </p>
                   
                               </td>
                             </tr>
                   
                             <!-- FOOTER com links úteis -->
                             <tr>
                               <td style=""background:#f8fafc; padding:24px 40px; border-top:1px solid #edf2f7;"">
                                 <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                   <tr>
                                     <td style=""font-size:13px; color:#a0aec0; text-align:center;"">
                                       <p style=""margin:0 0 8px; font-size:13px;"">
                                         📍 <a href=""#"" style=""color:#2EC4B6; text-decoration:none;"">github.com/LealLara</a> &nbsp;•&nbsp; 
                                         📧 <a href=""mailto:myfavoritepetshopstore@gmail.com"" style=""color:#2EC4B6; text-decoration:none;"">myfavoritepetshopstore@gmail.com</a>
                                       </p>
                                       <p style=""margin:0; font-size:12px; color:#cbd5e0;"">
                                         © 2026 Pet Store • Todos os direitos reservados
                                       </p>
                                       <p style=""margin:6px 0 0; font-size:11px; color:#cbd5e0;"">
                                         Feito com 💙 para você e seu pet
                                       </p>
                                     </td>
                                   </tr>
                                 </table>
                               </td>
                             </tr>
                   
                           </table>
                   
                           <!-- mensagem extra de rodapé -->
                           <p style=""margin:20px 0 0; font-size:12px; color:#a0aec0; text-align:center;"">
                             Este e-mail foi enviado para você. • <a href=""#"" style=""color:#a0aec0;"">Cancelar inscrição</a>
                           </p>
                   
                         </td>
                       </tr>
                     </table>
                   
                   </body>
                </html>";

            return mailObj;
        }
    }
}