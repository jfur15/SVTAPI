To run the API:
    Go into the folder /SVTAPI/bin/Debug/net6.0/ and in a console run:

        dotnet SVTAPI.dll

    This will start the server hosting the API on localhost:5000 or localhost:5001 

To test the API:
    Send a POST request to http://localhost:5000/api/robots/closest/ with a JSON body that looks like:
    {
        "loadId": 4, 
        "x": 2, 
        "y": 9
    }

    This should return a JSON object containing info about the closest robot to that location:
    {
        "robotId": 84,
        "distanceToGoal": 4.472,
        "batteryLevel": 77
    }

If there was more time:
    I would want the app to give more detailed error messages, e.g. that it couldn't find any robots near that location. So it would exit the program flow early when it finds that this is the case before it gives a generic error.

    I would want this endpoint to have a optional parameter for the distance you want to search, if you need to search a wider/smaller area for robots. It would be an integer defaulted to 10, and it would be plugged into the equation for checking the robots's distances from the coordinates.





