param([string]$chennel="starladder1")
& 'C:\Program Files (x86)\Livestreamer\livestreamer.exe' twitch.tv/$chennel source --player "'C:\Program Files (x86)\MPC-HC\mpc-hc.exe' /new" --http-header Client-ID=7bjbjzp3jzq3zc2n55epfoih9s54zn
