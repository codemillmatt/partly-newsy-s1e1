# Welcome to Partly Cloudy

I'm happy to announce today a new series that I started on the [Xamarin YouTube Channel](https://www.youtube.com/user/XamarinVideos) called [Partly Cloudy](https://www.youtube.com/watch?v=PAVb73wD4TY&list=PLM75ZaNQS_FYeMHheKdscYMxRVqVtPeto)!

In each _season_ of Partly Cloudy, you'll learn how to build an app from scratch and take it all the way through deploying it to the App Stores. And with a name like Partly Cloudy - we'll explore topics like [Azure](https://azure.microsoft.com/free?WT.mc_id=mobile-0000-masoucou) and [DevOps](https://azure.microsoft.com/services/devops?WT.mc_id=mobile-0000-masoucou) along with [Xamarin development](https://docs.microsoft.com/xamarin/?WT.mc_id=mobile-0000-masoucou) ... you know ... Partly Cloudy!

![Partly Cloudy Logo](https://res.cloudinary.com/code-mill-technologies-inc/image/upload/bo_1px_solid_rgb:000000,c_scale,h_600,r_20/v1571192518/PC-Header_busw9j.jpg)

In the first season, we'll build up a clone of the Microsoft News app!

## The Shows

Each week we'll cover a different aspect of mobile cloud development that's needed to build up our application.

One week we'll be building an [Azure Function backend](https://docs.microsoft.com/azure/azure-functions/?WT.mc_id=mobile-0000-masoucou). The next we'll be diving in on [Xamarin.Forms Shell](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/shell/?WT.mc_id=mobile-0000-masoucou). Then we'll be off and running with [App Center Auth](https://docs.microsoft.com/appcenter/auth/?WT.mc_id=mobile-0000-masoucou) or even doing some DevOps.

My aim is to keep each episode of Partly Cloudy to 15 minutes or less. 

The shows themselves serve as a gateway for the topic of the week. Just enough to whet your appetite, get you started in the right direction, and show you some landmarks along the way.

Because I'm keeping the episodes so short, I won't be able to go deep into every single topic that each episode covers ... that's what these blogs, articles, and readmes are for.

If there's anything in the episode that needs more explanation - I'll cover it in an article like this. This way you'll have a step-by-step guide to get your app up and running! And the each episode's full code will be on [GitHub](https://github.com/codemillmatt/partly-newsy-s1e1).

And as always, if you have any questions, or would like any topic from a particular episode expanded upon - please drop me a line on Twitter at [@codemillmatt](https://twitter.com/codemillmatt) - my DMs are always open!

## Episode 1: Hello News

[Episode 1](https://channel9.msdn.com/Shows/Partly-Cloudy/Hello-News-Intro-project-structure-and-HTTP-requests?term=partly cloudy&WT.mc_id=mobile-0000-masoucou) is all about getting things up and running. File->New for a Xamarin app.

We move fast though! Before the episode is over, our soon-to-be Microsoft News clone is invoking an Azure Function that's running locally!

And that's where I want to expand a bit more. So grab yourself some [Free Azure](https://azure.microsoft.com/free?WT.mc_id=mobile-0000-masoucou) and settle in for some fun!

First thing I want to talk about is installing the Azure Functions Core Tools locally.

And then how to modify your iOS and Android applications to invoke the Function without it being served over HTTPS!

### Azure Functions Core Tools

First off - the official documentation on installing the [Functions Core Tools lives here](https://docs.microsoft.com/azure/azure-functions/functions-run-local?WT.mc_id=mobile-0000-masoucou).

Fwiw ... You want to run the **2.x** version of the tools.

Got everything downloaded & installed? Good!

If you followed along with the video and used VS Code (or Visual Studio) to create the Function App from scratch, you'll be good to go.

But... if you're cloning my repo and starting from there, you'll need to add a `local.settings.json` file.

Create a file by that name, and add this to it:

```language-json
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "AzureWebJobsStorage": ""
  },
  "ConnectionStrings": {}
}
```

You should now be able to run and debug your Azure Function!

### iOS HTTP Local HTTP Debugging

By default, iOS does not let you call out to any web service that is not served over HTTPS.

And by default, our Azure Function is not served over HTTPS.

But not to worry - there's a relatively easy workaround for this that doesn't involve creating or trusting any certificates!

Open up the iOS's `Info.plist` file in a text editor - and add the following within the top level `<dict></dict>` element.

```language-xml
<key>NSAppTransportSecurity</key>
<dict>
    <key>NSAllowsLocalNetworking</key>
    <true/>
</dict>
```

That's going to allow iOS to load up any content found locally regardless of its SSL state.
    
Good for us!

### Android Local HTTP Debugging

Android suffers from the same fate that iOS does. No loading of non-secure content! (It's actually a good thing.)

But Android is a little more difficult than iOS. Android 9 (API 28) and later requires SSL. And it just so happens that's what we're targeting with this project!

My good friend James Montemagno [wrote a great post](https://devblogs.microsoft.com/xamarin/cleartext-http-android-network-security/?WT.mc_id=mobile-0000-masoucou) on how to allow local network traffic on an Android emulator.

Read the post for the full details - but essentially you have to add a special XML file and then specify in there to allow traffic on IP address `10.0.2.2` to go through in clear text.

Why `10.0.2.2`? Because that's the address that the Android emulator sees its local traffic come through as!

BUT THAT'S NOT ALL!

We also have to change the URL of the function from `http://localhost:7071` to `http://10.0.2.2:7071` ... cuz that's what the Android emulator is using for it's local loopback.

To do that - use some [Xamarin.Essentials goodness](https://docs.microsoft.com/xamarin/essentials/device-information?WT.mc_id=mobile-0000-masoucou)!

```language-csharp
if (DeviceInfo.DeviceType == DeviceType.Virtual && DeviceInfo.Platform == DevicePlatform.Android)
    FunctionUrl = "http://10.0.2.2:7071/api";
```

### Summing It Up

So episode 1 of Partly Cloudy is in the books!

Xamarin.Forms File->New. The project's structure explained. Azure Functions. And having our application make an HTTP call out to an Azure Function ... running on our local machine!!

That was a lot of ground for an inaugrual episode! Can't wait to see you next week when we bring in the Bing News Search API!
