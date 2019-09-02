import MemeTvApi from "/assets/js/api.js";

const app = document.querySelector(".app");

const loadVideo = async (id) => {
    const subtitles = await MemeTvApi.getSubtitlesAsync(id);
    const data = await subtitles.json();
    document.write(JSON.stringify(data));
    const video = document.createElement("video");
    const vtt = document.createElement("track");
    vtt.label = "English";
    vtt.kind = "subtitles";
    vtt.srclang = "en";
    vtt.src = data.vtt;
    vtt.default="default";

    const sourceMp4 = document.createElement("source");
    const sourceWebM = document.createElement("source");
    sourceMp4.src = data.mp4;
    sourceWebM.src = data.webm;
    sourceMp4.type = 'video/mp4';
    sourceWebM.type = 'video/webm';    

    video.appendChild(sourceMp4);
    video.appendChild(sourceWebM);
    video.appendChild(vtt);
    app.appendChild(video);
};

var url = new URL(window.location);
var id = url.searchParams.get("id");
if (id) {
    loadVideo(id);
}
