# Kinect Photos
Kinect Photos is a photo gallery browsing app built for Windows 7, 8, 10 & 11 using WPF, a .NET UI framework. It uses  SQLite to manage different users, photo albums, and file locations. Kinect Photos is currently *under early development*.

## Features
- Hands-free browsing using Kinect gestures
- Large display support
- Multi-user support with individual profiles
- Organized photo gallery display
- Zoom and view individual images


## Installation
To install Kinect Photos, you need to install the [Kinect for Windows SDK 1.8](https://www.microsoft.com/en-us/download/details.aspx?id=40278) first. Upon release, you can download the .exe from the *Releases* tab.

## Progress
I aim to complete the following before releasing the first version.
| Feature                        | Status         | Notes                                                                                                   |
|-------------------------------|----------------|---------------------------------------------------------------------------------------------------------|
| Core User Experience          |                |                                                                                                         |
| - User Profile Creation       | 🔄 In Progress | Basic name entry and saving. Profile picture and photo directory setup are pending.                    |
| - User Login                  | ✅ Done        | Users can select from existing profiles to log in.                                                     |
| - Profile Management          | 🔄 In Progress | Deletion is partially implemented; editing and full PFP/directory management are not started.          |
| - Photo Gallery Display       | ✅ Done        | Loads and displays images from configured directories.                                                 |
| - Image Viewing               | 🔄 In Progress        | Users can navigate to a full-screen view of an individual image and zoom in; panning not yet functional.                                         |
| Kinect Interaction            |                |                                                                                                         |
| - Sensor Integration          | ✅ Done        | Application initializes and manages the Kinect sensor.                                                 |
| - Gesture Navigation & Control| 🔄 In Progress | Hand tracking and gestures for scrolling, selecting, and zooming are implemented. Advanced hand tracking gestures (throwing, rotating, etc.) are not yet implemented. |
| Application Features          |                |                                                                                                         |
| - In-App Settings             | ❌ Not Started | Inserted placeholders for application settings (e.g., UI preferences, Kinect settings).                          |
| - UI Customization            | ❌ Not Started | UI theme or layout customization options. (Post-Release Feature)                                            |
| - Gallery Enhancements        | ❌ Not Started | Future improvements planned: performance optimization for large galleries, image caching, sorting, etc.|
| - Search        | ❌ Not Started | No image indexing, search, sort features available. (Post-Release Feature) |
| Database SQLite Backend            |         |                                |
| Database Tables Created            | ✅ Done        | Created SQLite database for storing profiles, directory paths, and albums.                                   |
| - User Management            | ✅ Done        | Users successfully managed, added, and deleted in database.                               |
| - File Path Management            | 🔄 In Progress        | Filepaths can be read, but not added.                                  |
| - Album Management            | ❌ Not Started        | Albums not  yet implemented.                            |


### Screenshots & Demo
Coming soon!

## Built With
- C# and WPF (.NET Framework 4.0)
- Kinect for Windows SDK 1.8
- Kinect Studio 1.8 (for gesture/app flow testing)
- SQLite for local database storage
- Dapper (for lightweight ORM/data access)

## Contributing
Contributions are welcome once the project stabilizes! For now, feel free to open issues or suggest features.
