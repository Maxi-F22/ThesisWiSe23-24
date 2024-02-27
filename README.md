# ThesisWiSe23-24

Die Unity-Projekte in diesem Repository entstanden im Rahmen meiner Bachelor Thesis an der Hochschule Furtwangen.

Das Projekt im ersten Ordner <i>MultiUserMusicVR</i> beinhaltet das eigentliche Projekt. 
Hierbei handelt es sich um eine Multi-User-VR Erfahrung mit Fokus auf gemeinsames Erschaffen von Musik. 
Diese wurde auf der Werkschau der Fakultät Digitale Medien im Januar 2024 von Besucherinnen und Besuchern getestet.
Es gibt hier zwei Arten von Instrumenten, einmal die Beatbälle und einmal die Klangwand.
Die Beatbälle können von ihrer Position aufgehoben und geworfen werden. Nachdem sie den Boden berühren, beginnen sie unendlich auf und ab zu hüpfen und bei jeder Berührung mit dem Boden einen Ton abzuspielen.
An der Klangwand befinden sich leuchtende Objekte, die durchgängig einen Ton abspielen. Sie können gegriffen und an der Wand entlang bewegt werden, wobei die Lautstärke und Tonhöhe von der Position abhängt.
Ein weiteres Hauptaugenmerk der Anwendung war das haptische Feedback, das bei den einzelnen Instrumenten unterschiedlich umgesetzt wurde. 
Die Anwendung ist darauf ausgelegt, auf zwei Standalone VR-Headsets gespielt zu werden.

Das Projekt im zweiten Ordner <i>MultiUserMusicVR</i> beinhaltet ein Template für das eigene Erstellen von Multiplayer-VR-Anwendungen für das lokale Netzwerk.
Dabei handelt es sich um ein Projekt, in dem nur die nötigsten Komponenten für ein funktionierendes Netzwerkspiel enthalten sind.
Zusätzlich wurde eine Beispielwelt mit einem Cube als Kollisionsobjekt und einer Sphere, um die Interaktion zu testen, eingefügt.


## Installation
1. Öffne das Projekt in ihrem Unity-Launcher. 
2. Stelle in den Build Settings auf die Plattform Android um.
3. Passe die OpenXR Einstellungen in den Player Settings auf das jeweilige Endgerät an
4. Passe in der C#-Datei "NetworkStartup" die IP-Adresse auf die des Hostgeräts an
5. Builde das Android-APK und installiere es auf dem Headset
6. In der Anwendung, wähle mit dem Hostgerät den Button "Host" aus und mit den Client-Geräten den Button "Client"


## Credits
Oculus Hands Physics Package by <a href="https://www.youtube.com/@ValemTutorials">Valem Tutorials</a>