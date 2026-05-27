<b>THIS README IS WIP SO IT WILL BE CHANGED SOON</b>

<a id="readme-top"></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![License][license-shield]][license-url]

<br />
<div align="center">
  <a href="https://github.com/3060s/FruityScale">
    <img src="FruityScale.Infrastructure/Resources/favicon.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">FruityScale</h3>

  <p align="center">
    A smart companion application that analyzes your FL Studio Piano Roll notes to instantly identify matching musical scales.
    <br />
    <a href="https://github.com/3060s/FruityScale"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/3060s/FruityScale">View Demo</a>
    &middot;
    <a href="https://github.com/3060s/FruityScale/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    &middot;
    <a href="https://github.com/3060s/FruityScale/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>

<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li><a href="#getting-started">Getting Started</a></li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

## About The Project

FruityScale is a desktop application designed to bridge the gap between your DAW and music theory. It currently integrates with FL Studio via a lightweight, internal script. 

With just few clicks, FruityScale retrieves note data directly from your current Piano Roll and instantly analyzes which musical scales those notes belong to. It is a quick and seamless way to stay in key, analyze complex chords, or figure out where to take your melodies next.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

* [![C#][CSharp-shield]][CSharp-url]
* [![.NET][DotNet-shield]][DotNet-url]
* [![AvaloniaUI][Avalonia-shield]][Avalonia-url]

> **Note:** This project also utilizes **Serilog** for robust logging and **xUnit** for comprehensive unit testing.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Getting Started

Currently, FruityScale is distributed as pre-compiled, ready-to-run executables for major operating systems. You do not need to build the project from source unless you want to contribute.

To get started:
1. Head over to the **[Releases](https://github.com/3060s/FruityScale/releases)** page.
2. Download the latest version corresponding to your operating system.
3. Extract and run the application.

*(Detailed installation instructions and FL Studio script setup guide will be added in future releases).*

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Usage

1. Open your project in FL Studio and add some notes to the Piano Roll.
2. Run the provided FruityScale internal script inside FL Studio.
3. Open the FruityScale desktop app and click the fetch button.
4. The application will instantly display all compatible musical scales based on your notes.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Roadmap

- [x] Initial FL Studio script integration
- [x] One-click note data fetching
- [x] Basic scale matching algorithm
- [ ] Support for other popular DAWs (Ableton, Logic Pro, Reaper, etc.)
- [ ] Advanced analysis tools and extended chord detection
- [ ] Experimental: Live MIDI input tracking with real-time scale detection

See the [open issues](https://github.com/3060s/FruityScale/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Top contributors:

<a href="https://github.com/3060s/FruityScale/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=3060s/FruityScale" alt="contrib.rocks image" />
</a>

## License

Distributed under the GPL-3.0 License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Contact

Project Link: [https://github.com/3060s/FruityScale](https://github.com/3060s/FruityScale)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

[contributors-shield]: https://img.shields.io/github/contributors/3060s/FruityScale.svg?style=for-the-badge
[contributors-url]: https://github.com/3060s/FruityScale/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/3060s/FruityScale.svg?style=for-the-badge
[forks-url]: https://github.com/3060s/FruityScale/network/members
[stars-shield]: https://img.shields.io/github/stars/3060s/FruityScale.svg?style=for-the-badge
[stars-url]: https://github.com/3060s/FruityScale/stargazers
[issues-shield]: https://img.shields.io/github/issues/3060s/FruityScale.svg?style=for-the-badge
[issues-url]: https://github.com/3060s/FruityScale/issues
[license-shield]: https://img.shields.io/github/license/3060s/FruityScale?style=for-the-badge
[license-url]: https://github.com/3060s/FruityScale/blob/main/LICENSE

[CSharp-shield]: https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white
[CSharp-url]: https://learn.microsoft.com/en-us/dotnet/csharp/
[DotNet-shield]: https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white
[DotNet-url]: https://dotnet.microsoft.com/
[Avalonia-shield]: https://img.shields.io/badge/AvaloniaUI-33A3E3?style=for-the-badge&logo=dotnet&logoColor=white
[Avalonia-url]: https://avaloniaui.net/
