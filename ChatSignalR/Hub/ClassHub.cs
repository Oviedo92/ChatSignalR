using Microsoft.AspNetCore.SignalR;
using ChatSignalR.Models; // Si decides poner el modelo Turno en una carpeta Models
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChatSignalR.Hubs
{
    public class ClassHub : Hub
    {
        public async Task VerId()
        {
            var id = Context.ConnectionId;
            await Clients.Caller.SendAsync("VerId", id);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        private static List<Turno> turnos = new();
        private static Turno turnoActual = null;
        private static int contador = 1;

        public async Task AgendarTurno(string nombre)
        {
            var turno = new Turno { Id = contador++, Nombre_Cliente = nombre };
            turnos.Add(turno);
            await Clients.All.SendAsync("ActualizarTurnos", turnos, turnoActual);
        }

        public async Task AtenderTurno()
        {
            if (turnos.Count == 0) return;

            turnoActual = turnos[0];
            turnos.RemoveAt(0);
            await Clients.All.SendAsync("ActualizarTurnos", turnos, turnoActual);
            await Clients.Client(Context.ConnectionId).SendAsync("NotificarTurno", turnoActual);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ActualizarTurnos", turnos, turnoActual);
        }
    }
}
