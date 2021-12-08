# Load Generator

See [project instructions](src/mParticle.LoadGenerator/README.txt) for background.

Note that hackerrank generated the project using dotnet 3 which is a deprecated version. I converted the project to be dotnet 5, which is a newer version that I got through `brew install dotnet`.

### Testing

```
dotnet test --results-directory ./reports/
```

### Running the app

```
dotnet build;dotnet run --project src/mParticle.LoadGenerator src/mParticle.LoadGenerator/config.json
```

# Retrospective

This project was pretty interesting. This was my first time ever seeing C# code. I've never had any interaction with C# at all in the past. Naturally this was a lot to try to do all at once, haha.

Overall I am satisfied. I delivered most of the project requirements. It did take me longer than the estimated 2 hours, but that's likely because of my level of test coverage and the learning curve of C#.

## The good

I am pleased with the user experience working with the tool. The tool runs a configurable amount of cycles until you interrupt it with control+c. Then it prints a report of how it did. I'm also pretty satisfied with how I handled the async stuff.

My unit test coverage is pretty good too. I used Test Driven Development when building this, which I feel like helped me a lot. Especially since I've never used C# before it was a little tricky to figure out where to begin so I just started writing tests and the rest came naturally.

## The bad

This is my first time ever working with or even seeing C# code at all. I am certain I didn't follow proper conventions. I come from a java background so it wasn't terribly hard to get coding but there was definitely a learning curve.

## The ugly

I couldn't get the AWS API to return 200 for my request. I kept getting http 400 with response:

```json
{ "error": "correctly formatted json body is required" }
```

Since the error woudln't tell me why my json was considered wrong and I already invested more time than expected into this take-home project, I figured I'd not try to go any further on that.
