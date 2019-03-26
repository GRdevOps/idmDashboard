# idmDashboard
This project is meant to serve as a NetIQ IDM Dashboard for Drivers and servers.  Currently only Drivers are configured but I am looking to add additional functionality.  The website is developed in ASP.NET Core using Entity Framework Core to talk to a backend SQL Server database.  The project that I have included is a standalone project that includes an SQL script to setup the initial config for the database, but feel free to deploy using any method you are comfortable with.  If you would prefer to use a different database that is entirely possible, but will require some code changes on your part.

I have included 3 example bash scripts.  These are meant to server as a template, but will need configuring to work.  The first is used to Query Servers for driver information.  I pass in the name of the server and it spits back a file with the driver information contained in it.  The second script is for sending the web requests, it first pulls in the file the first script created.  It then loops through the file composing curl requests for each driver.  The third script is what I have running as a service, it simply loops through each server and runs the above two scripts.

Additional configuration is required and you should have a basic understanding of several topics to make this easier:

-LDAP
-Scripting (Linux or Windows depending on your environment)
-Web Servers
-Web calls against website API's

I will be adding some "How To" guides but below is a brief explanation of my setup, however feel free to be creative :)

I am running this on a Linux box as a standalone application hosted on .Net core.  The webserver that is running it is kestrel, with Apache acting as a reverse proxy in front of it.  The backend database is simply an sql server express box running on linux.

I have scripts running locally that queries data from LDAP and then compiles that into web calls that are made using curl against 127.0.0.1 to update the database through the website post API.  Both Kestrel and the web calls are running through service files in systemd.

I then have a notification system that we use running a http sensor that calls out to the get API against each driver to ensure they are up and running.
