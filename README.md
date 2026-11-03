<div align="center">

# 🧪 Physics Playground

8 interactive physics simulations built in Unity. Change the inputs, watch what happens, then learn why.

![demo](https://img.shields.io/badge/demo-live-brightgreen)
![Unity](https://img.shields.io/badge/Unity-6-black?logo=unity)
![C#](https://img.shields.io/badge/C%23-512BD4?logo=csharp&logoColor=white)
![WebGL](https://img.shields.io/badge/WebGL-990000?logo=webgl&logoColor=white)
![visitors](https://visitor-badge.laobi.icu/badge?page_id=Codoxide.physicsplayground)

</div>

---

## Live Demo

[playphysics.vercel.app](https://playphysics.vercel.app/)

## Preview

| Main Menu | Explanation Panel |
|:---:|:---:|
| ![Main Menu](screenshots/menu.png) | ![Explanation Panel](screenshots/explanation.png) |

| Lenz's Law | Rollercoaster | Inclined Planes | Table Pulley |
|:---:|:---:|:---:|:---:|
| ![Lenz's Law](screenshots/lenzs-law.png) | ![Rollercoaster](screenshots/rollercoaster.png) | ![Inclined Planes](screenshots/inclined-planes.png) | ![Table Pulley](screenshots/table-pulley.png) |
| **Atwood Pulley** | **Projectile Motion** | **Air Columns** | **Doppler Effect** |
| ![Atwood Pulley](screenshots/atwood-pulley.png) | ![Projectile Motion](screenshots/projectile-motion.png) | ![Air Columns](screenshots/air-columns.png) | ![Doppler Effect](screenshots/doppler-effect.png) |

## Features

- **8 simulations** covering forces, energy, waves and electromagnetism
- **Live controls**: sliders, toggles and readouts that update as the simulation runs
- **Pause** at any moment to look closely at what's happening
- **Explanation panels** in every simulation with a diagram, formulas, a worked example and a fun fact, written for grade 11 physics
- **Runs in the browser**: no download or install

## Simulations

| Simulation | What you can explore |
|---|---|
| Lenz's Law | Move a magnet through a coil and watch the galvanometer react |
| Rollercoaster | Potential energy turning into kinetic energy, with and without friction |
| Inclined Planes | How ramp angle and friction change the forces and acceleration |
| Table Pulley | Tension and acceleration of a block pulled by a hanging mass |
| Atwood Pulley | Two masses over a pulley, equal and unequal |
| Projectile Motion | How launch height and speed change flight time and distance |
| Air Columns | Resonant frequency from tube length, temperature and harmonic number |
| Doppler Effect | Wavefronts bunching up ahead of a moving sound source |

## Tech

Unity 6 and C#, exported as a WebGL build with a custom web template. Hosted on Vercel. Made for desktop browsers, so phones and tablets see a short message instead.

## Project Structure

```
physicsplayground/
├── Assets/
│   ├── Scenes/
│   ├── Scripts/
│   └── WebGLTemplates/
│       └── PhysicsPlayground/
├── Final Build/
├── screenshots/
├── Packages/
└── ProjectSettings/
```

## Running Locally

Open the project in **Unity 6**, load the main menu scene and press **Play**. Set the Game View to **1600 x 820** to match the website.

To run the web build instead:

```bash
git clone https://github.com/Codoxide/physicsplayground.git
cd "physicsplayground/Final Build"
python3 -m http.server
```

Then open `localhost:8000`.

---

<div align="center">

Built by Codoxide

</div>
