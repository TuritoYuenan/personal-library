# UML Class Diagram

```mermaid
classDiagram
direction TB

Program *-- Shelf
Program *-- Settings
Program *-- UserInterface

Shelf o-- Material
Material <|-- Book
Material <|-- Article
Material <|-- Video
Material <|-- Webpage
Article *-- ANumbers

Article ..|> IAvailableOnline: implements
Webpage ..|> IAvailableOnline: implements
Video ..|> IAvailableOnline: implements

UserInterface <.. CreateButton
UserInterface o-- IPage
IPage <|.. MaterialPage: implements
IPage <|.. SettingsPage: implements
IPage <|.. ShelfPage: implements

class Program {
	+ Main()$
	- SaveData(Shelf, Settings)$
	- LoadData(Shelf, Settings)$
}

%% Interfaces

class IAvailableOnline {
	<<Interface>>
	+ OpenLink()
}
class IPage {
	<<Interface>>
	+ Title: string ~~RO property~~
	+ Render()
}

%% Models

class Settings {
	<<Singleton>>
	- _instance: Settings$
	- _padlock: object$
	+ DarkMode: bool ~~property~~
	+ SavePath: string ~~property~~
	+ Instance: Settings ~~RO property~~
	- Settings()
}
class Shelf {
	+ Contents: ListMaterial ~~RO property~~
	+ this[int key]: Material ~~indexer~~
	+ Shelf()
	+ Add(Material)
	+ Remove(Material) bool
	+ Clear()
	+ GetRack(int) List~Material~
	+ ToJsonList() List~Json~
}
class Material {
	<<Abstract>>
	+ Authors: string[] ~~property~~
	+ Title: string ~~property~~
	+ Date: DateOnly ~~property~~
	+ Material(...)
	+ Material(Json)
	+ ToJson() Json
	+ GetID()*
}
class Book {
	+ Publisher: string ~~property~~
	+ Edition: string ~~property~~
	+ ISBN: string ~~property~~
	+ Book(...)
	+ Book(Json)
	+ GetID()
}
class Article {
	- _pages: Tuple~int, int~
	+ Publisher: string ~~property~~
	+ Numbers: int ~~property~~
	+ DOI: string ~~property~~
	+ Article(...)
	+ Article(Json)
	+ GetID()
	+ OpenLink()
}
class Webpage {
	+ Website: string ~~property~~
	+ Url: Uri ~~property~~
	+ Webpage(...)
	+ Webpage(Json)
	+ GetID()
	+ OpenLink()
}
class Video {
	+ Platform: string ~~property~~
	+ Url: Uri ~~property~~
	+ Video(...)
	+ Video(Json)
	+ GetID()
	+ OpenLink()
}
class ANumbers {
	<<Tuple type>>
	int Volume
	int Issue
	int PageS
	int PageE
}

%% Views

class CreateButton {
	<<Factory>>
	+ ViewOnlineButton(int, int) bool
	+ TopBarButton(int, int, string) bool
}
class UserInterface {
	<<Singleton>>
	- _instance: UserInterface$
	- _padlock: object$
	- _page: Stack~IPage~
	+ Window: Window ~~RO property~~
	+ Buttons: Dictionary~string, bool~ ~~RO property~~
	+ CurrentPage: IPage ~~RO property~~
	+ IsStartPage: bool ~~RO property~~
	- UserInterface()
	+ GetInstance() UserInterface$
	+ GoInto(IPage)
	+ GoBack() IPage
	+ Render()
	- TopBar()
}
class MaterialPage {
	- material: Material
	+ Title: string ~~RO property~~
	+ MaterialPage(Material)
	+ Render()
	+ ViewOnline(Uri)
}
class SettingsPage {
	- settings: Settings
	+ Title: string ~~RO property~~
	+ SettingsPage()
	+ Render()
}
class ShelfPage {
	- shelf: Shelf
	+ Title: string ~~RO property~~
	+ ShelfPage(Shelf)
	+ Render()
	- ShelfRack(int, int)
	- MaterialCard(int, int, Material)
}
```
