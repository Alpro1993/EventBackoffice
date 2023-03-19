using EventBackofficeBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBackofficeBackend.Data;
public static class DbInitializer
{
    public static void Initialize(EventBackofficeBackendContext context)
    {

        if (context.Events.Any()) {
            return;
        }
        context.Database.EnsureCreated();

        var venues = new List<Venue>
        {
            new Venue { Name = "Convention Center", Location = "https://www.example.com/convention-center" },
            new Venue { Name = "Expo Hall", Location = "https://www.example.com/expo-hall" },
            new Venue { Name = "Conference Center", Location = "https://www.example.com/conference-center" },
        };

        context.Venues.AddRange(venues);
        context.SaveChanges();

        var rooms = new List<Room>
        {
            new Room { Name = "Room A", Venue = venues[0] },
            new Room { Name = "Room B", Venue = venues[0] },
            new Room { Name = "Room C", Venue = venues[1] },
            new Room { Name = "Room D", Venue = venues[2] },
        };

                // Event data
        var events = new Event[]
        {
            new Event
            {
                Name="Tech Conference 2023", 
                StartDate=new DateTime(2023, 6, 5), 
                EndDate=new DateTime(2023, 6, 7),
                Venues = venues.Take(3).ToList()
            },
            new Event
            {
                Name="Startup Summit 2023",
                StartDate=new DateTime(2023, 8, 15),
                EndDate=new DateTime(2023, 8, 16),
                Venues = venues.Take(1).ToList()
            },
            new Event
            {
                Name="Big Data Symposium 2023", 
                StartDate=new DateTime(2023, 10, 18),
                EndDate=new DateTime(2023, 10, 20),
                Venues = venues.Take(2).ToList()
            }
        };
    
        foreach (Event e in events)
        {
            context.Events.Add(e);
        }
        context.SaveChanges();

        context.Rooms.AddRange(rooms);
        context.SaveChanges();

        // var sponsors = new List<Sponsor> {
        //     new Sponsor
        //     {
        //         Name = "ABC Corp",
        //         Logo = "https://www.example.com/abc-logo.png",
        //         FloorSpace = 100,
        //         URL = "https://www.example.com/abc",
        //         Representatives = new List<Person>
        //         {
        //             new Person
        //             {
        //                 Name = "John Smith",
        //                 Picture = "https://www.example.com/john-smith.png",
        //                 Bio = "John is a marketing executive at ABC Corp",
        //                 Email = "john.smith@example.com"
        //             },
        //             new Person
        //             {
        //                 Name = "Jane Doe",
        //                 Picture = "https://www.example.com/jane-doe.png",
        //                 Bio = "Jane is a sales executive at ABC Corp",
        //                 Email = "jane.doe@example.com"
        //             }
        //         },
        //         Sessions = new List<Session>
        //         {
        //             new Session
        //             {
        //                 Name = "ABC Corp Presentation",
        //                 StartDate = new DateTime(2023, 04, 01, 10, 00, 00),
        //                 EndDate = new DateTime(2023, 04, 01, 11, 00, 00),
        //                 EventID = events[0].EventID
        //             }
        //         }
        //     },
        //     new Sponsor
        //     {
        //         Name = "XYZ Inc",
        //         Logo = "https://www.example.com/xyz-logo.png",
        //         FloorSpace = 50,
        //         URL = "https://www.example.com/xyz",
        //         Representatives = new List<Person>
        //         {
        //             new Person
        //             {
        //                 Name = "Bob Johnson",
        //                 Picture = "https://www.example.com/bob-johnson.png",
        //                 Bio = "Bob is a marketing executive at XYZ Inc",
        //                 Email = "bob.johnson@example.com"
        //             }
        //         },
        //         Sessions = new List<Session>
        //         {
        //             new Session
        //             {
        //                 Name = "XYZ Inc Presentation",
        //                 StartDate = new DateTime(2023, 04, 01, 14, 00, 00),
        //                 EndDate = new DateTime(2023, 04, 01, 15, 00, 00),
        //                 EventID = events[0].EventID
        //             }
        //         }
        //     }
        // };

        // context.Sponsors.AddRange(sponsors);
        // context.SaveChanges();
        
        // var sessions = new List<Session>
        // {
        //     new Session
        //     {
        //         Name = "Opening Keynote",
        //         StartDate = DateTime.Parse("2023-05-01 09:00:00"),
        //         EndDate = DateTime.Parse("2023-05-01 10:00:00"),
        //         Room = rooms[0],
        //         Sponsors = new List<Sponsor>() {sponsors[0]},
        //         EventID = events[0].EventID,
        //         Speakers = new List<Speaker>
        //         {
        //             new Speaker { Name = "John Smith", Email = "john.smith@example.com" },
        //             new Speaker { Name = "Jane Doe", Email = "jane.doe@example.com" }
        //         },
        //         Papers = new List<Paper>
        //         {
        //             new Paper { Title = "The Future of AI", File = "https://example.com/future-of-ai.pdf" }
        //         }
        //     },
        //     new Session
        //     {
        //         Name = "Lunch Break",
        //         StartDate = DateTime.Parse("2023-05-01 12:00:00"),
        //         EndDate = DateTime.Parse("2023-05-01 13:00:00"),
        //         Room = rooms[2],
        //         EventID = events[0].EventID,
        //         Sponsors = new List<Sponsor>() {sponsors[0], sponsors[1]},
        //         Speakers = new List<Speaker>(),
        //         Papers = new List<Paper>()
        //     },
        //     new Session
        //     {
        //         Name = "Closing Keynote",
        //         StartDate = DateTime.Parse("2023-05-01 17:00:00"),
        //         EndDate = DateTime.Parse("2023-05-01 18:00:00"),
        //         Room = rooms[0],
        //         EventID = events[0].EventID,
        //         Sponsors = new List<Sponsor>(),
        //         Speakers = new List<Speaker>
        //         {
        //             new Speaker { Name = "Bob Johnson", Email = "bob.johnson@example.com" }
        //         },
        //         Papers = new List<Paper>
        //         {
        //             new Paper { Title = "The Future of Space Exploration", File = "https://example.com/future-of-space.pdf" }
        //         }
        //     }
        // };

        // foreach (var session in sessions)
        // {
        //     context.Sessions.Add(session);
        // }

        // var subsessions = new List<Session> 
        // {
        //     new Session
        //     {
        //         Name = "Closing Keynote",
        //         StartDate = DateTime.Parse("2023-05-01 17:00:00"),
        //         EndDate = DateTime.Parse("2023-05-01 18:00:00"),
        //         Room = rooms[0],
        //         EventID = events[0].EventID,
        //         Sponsors = new List<Sponsor>(),
        //         parentSession = sessions[0],
        //         Speakers = new List<Speaker>
        //             {
        //                 new Speaker { Name = "Bob Johnson", Email = "bob.johnson@example.com" }
        //             },
        //             Papers = new List<Paper>
        //             {
        //                 new Paper { Title = "The Future of Space Exploration", File = "https://example.com/future-of-space.pdf" }
        //             }
        //     },
        //     new Session
        //     {
        //         Name = "Keynote Address on Digital Transformation",
        //         StartDate = DateTime.Parse("2023-06-15 14:00:00"),
        //         EndDate = DateTime.Parse("2023-06-15 15:00:00"),
        //         Room = rooms[1],
        //         EventID = events[0].EventID,
        //         Sponsors = new List<Sponsor>
        //         {
        //             new Sponsor { Name = "ABC Corporation"},
        //             new Sponsor { Name = "XYZ Corp"}
        //         },
        //         parentSession = sessions[2],
        //         Speakers = new List<Speaker>
        //         {
        //             new Speaker { Name = "Amy Lee", Email = "amy.lee@example.com" }
        //         },
        //         Papers = new List<Paper>
        //         {
        //             new Paper { Title = "The Importance of AI in Business", File = "https://example.com/ai-business.pdf" }
        //         }
        //     },
        //     new Session
        //     {
        //         Name = "Panel Discussion: Climate Change and Sustainability",
        //         StartDate = DateTime.Parse("2023-07-20 10:00:00"),
        //         EndDate = DateTime.Parse("2023-07-20 11:30:00"),
        //         Room = rooms[2],
        //         EventID = events[0].EventID,
        //         Sponsors = new List<Sponsor>
        //         {
        //             new Sponsor { Name = "GreenTech Corp"}
        //         },
        //         parentSession = sessions[1],
        //         Speakers = new List<Speaker>
        //         {
        //             new Speaker { Name = "Dr. Jane Smith", Email = "jane.smith@example.com" },
        //             new Speaker { Name = "David Lee", Email = "david.lee@example.com" },
        //             new Speaker { Name = "Maria Garcia", Email = "maria.garcia@example.com" }
        //         },
        //         Papers = new List<Paper>()
        //     }

        // };
        // context.AddRange(subsessions);
        // context.SaveChanges();

        // var participants = new List<Participant> {
        //     new Participant 
        //     {
        //         Name = "John Doe",
        //         Email = "johndoe@example.com",
        //         Picture = "https://example.com/johndoe.jpg",
        //         Bio = "I am a software engineer with over 10 years of experience.",
        //         Visible = true,
        //         Event = events[0]
        //     },
        //     new Participant 
        //     {
        //         Name = "Jane Smith",
        //         Email = "janesmith@example.com",
        //         Picture = "https://example.com/janesmith.jpg",
        //         Bio = "I am a data analyst with a passion for visualizations.",
        //         Visible = false,
        //         Event = events[1]
        //     },
        //     new Participant 
        //     {
        //         Name = "Bob Johnson",
        //         Email = "bobjohnson@example.com",
        //         Picture = "https://example.com/bobjohnson.jpg",
        //         Bio = "I am a full-stack developer who loves building web applications.",
        //         Visible = true,
        //         Event = events[0]
        //     }
        // };

        // context.AddRange(participants);
        // context.SaveChanges();    
    }
}