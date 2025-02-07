# Apologies, this is currently completely over-engineered for what it is.
The only file you'll care about right now is the MoonBoardClient.cs

This does the logic to both get valid login cookies and then you can choose from the functions as to what you what.

The GetBenchmarks function gets all the benchmarks but not a lot of details, it's just a pretty model to display the list on the website.

Unfortunately the only place they load the full model right now is in the GetProblems by setter endpoint(at least from what I could find).

So I do this little fractal technique where I choose people with a lot of logs(in this case ravioli biceps, hoseok and ben) and find the setter Ids of everybody in their climbs and insert it into the database.
Once saved I call another endpoint to get all the climbs for each setter and insert them into the database.
