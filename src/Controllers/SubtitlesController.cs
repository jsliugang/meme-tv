﻿using System.Text;
using System.Threading.Tasks;
using MemeTV.BusinessLogic;
using MemeTV.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MemeTV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtitlesController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IClipManager clipManager;

        public SubtitlesController(ILogger logger, IClipManager clipManager)
        {
            this.logger = logger;
            this.clipManager = clipManager;
        }

        [HttpPost]
        public Task SaveSubtitlesAsync(SaveSubtitleModel model)
        {
            return clipManager.StoreAsync(model.Name, model.Email, model.Clip, model.Subtitles);
        }

        [HttpGet("{id}")]
        public Task<UserClip> GetSubtitlesAsync(string id)
        {
            return clipManager.GetClipSubtitleAsync(id);
        }

        [HttpGet("vtt/{id}")]
        public async Task<IActionResult> GetVtt(string id)
        {
            var vtt = await clipManager.GetClipVttAsync(id);
            var bytes = Encoding.UTF8.GetBytes(vtt);
            return File(bytes, "text/vtt");
        }
    }

    public class SaveSubtitleModel
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("clip")] public string Clip { get; set; }
        [JsonProperty("subtitles")] public string[] Subtitles { get; set; }
    }
}