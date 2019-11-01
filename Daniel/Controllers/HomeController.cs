using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Daniel.Models;
using System.IO;

namespace Daniel.Controllers
{
    public class HomeController : Controller
    {
        public TimeSpan TrataHora(string horario)
        {
            var position = horario.IndexOf(':');
            var hora = horario.Substring(0, position).Trim();
            var minuto = horario.Substring(position + 1).Trim();
            return new TimeSpan(Int32.Parse(hora), Int32.Parse(minuto), 0);
        }

        public List<string> availableHours(string filePath, string recievedHour)
        {
            List<string> listResult = new List<string>();

            using (var reader = new StreamReader(filePath))
            {
                bool passedFirstline = false;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (passedFirstline)
                    {
                        var position = values[1].IndexOf('-');
                        var abertoDe = values[1].Substring(0, position).Trim();
                        var abertoAte = values[1].Substring(position + 1).Trim();

                        //abertoDe
                        TimeSpan horarioDe = TrataHora(abertoDe);

                        //abertoAte
                        TimeSpan horarioAte = TrataHora(abertoAte);

                        //horaRecebida
                        TimeSpan horarioRecebida = TrataHora(recievedHour);

                        if (horarioRecebida >= horarioDe && horarioRecebida <= horarioAte)
                        {
                            listResult.Add(line);
                        }
                    }
                    else
                    {
                        passedFirstline = true;
                    }
                }
            }
            return listResult;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Result(FormModel model)
        {
            if (model.horario is null)
            {
                model.horario = "00:00";
            }
            return View(availableHours("./Data/restaurant-hours.csv", model.horario));

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
