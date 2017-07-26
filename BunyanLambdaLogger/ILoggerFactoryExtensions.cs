﻿using Microsoft.Extensions.Configuration;
using System;

// https://raw.githubusercontent.com/aws/aws-lambda-dotnet/144df7cb7988ae8fb007d5ea7e172d23faff0d5b/Libraries/src/Amazon.Lambda.Logging.AspNetCore/ILoggerFactoryExtensions.cs
// Same namespace as ILoggerFactory, to make these extensions appear
// without the user needing to including our namespace first.
namespace Microsoft.Extensions.Logging
{
  /// <summary>
  /// ILoggerFactory extensions
  /// </summary>
  public static class ILoggerFactoryExtensions
  {
    /// <summary>
    /// Adds a Lambda logger provider with default options.
    /// </summary>
    /// <param name="factory">ILoggerFactory to add Lambda logger to.</param>
    /// <returns>Updated ILoggerFactory.</returns>
    [CLSCompliant(false)] // https://github.com/aspnet/Logging/issues/500
    public static ILoggerFactory AddBunyanLambdaLogger(this ILoggerFactory factory)
    {
      var options = new LambdaLoggerOptions();
      return AddBunyanLambdaLogger(factory, options);
    }

    /// <summary>
    /// Adds a Lambda logger provider with specified options.
    /// </summary>
    /// <param name="factory">ILoggerFactory to add Lambda logger to.</param>
    /// <param name="options">Lambda logging options.</param>
    /// <param name="applicationName">The name of the application being logged.</param>
    /// <returns>Updated ILoggerFactory.</returns>
    [CLSCompliant(false)] // https://github.com/aspnet/Logging/issues/500
    public static ILoggerFactory AddBunyanLambdaLogger(this ILoggerFactory factory, LambdaLoggerOptions options,
      string applicationName)
    {
      if (factory == null)
      {
        throw new ArgumentNullException(nameof(factory));
      }
      if (options == null)
      {
        throw new ArgumentNullException(nameof(options));
      }

      var provider = new BunyanLambdaILoggerProvider(options, applicationName);
      factory.AddProvider(provider);
      return factory;
    }

    /// <summary>
    /// Adds a Lambda logger provider with specified options.
    /// </summary>
    /// <param name="factory">ILoggerFactory to add Lambda logger to.</param>
    /// <param name="options">Lambda logging options.</param>
    /// <returns>Updated ILoggerFactory.</returns>
    [CLSCompliant(false)] // https://github.com/aspnet/Logging/issues/500
    public static ILoggerFactory AddBunyanLambdaLogger(this ILoggerFactory factory, LambdaLoggerOptions options)
    {
      if (factory == null)
      {
        throw new ArgumentNullException(nameof(factory));
      }
      if (options == null)
      {
        throw new ArgumentNullException(nameof(options));
      }

      var provider = new BunyanLambdaILoggerProvider(options);
      factory.AddProvider(provider);
      return factory;
    }

    /// <summary>
    /// Adds a Lambda logger provider with options loaded from the "Lambda.Logging" subsection
    /// of the specified configuration.
    /// </summary>
    /// <param name="factory">ILoggerFactory to add Lambda logger to.</param>
    /// <param name="configuration">IConfiguration to use when construction logging options.</param>
    /// <returns>Updated ILoggerFactory.</returns>
    [CLSCompliant(false)] // https://github.com/aspnet/Logging/issues/500
    public static ILoggerFactory AddBunyanLambdaLogger(this ILoggerFactory factory, IConfiguration configuration)
    {
      if (factory == null)
      {
        throw new ArgumentNullException(nameof(factory));
      }
      if (configuration == null)
      {
        throw new ArgumentNullException(nameof(configuration));
      }

      var options = new LambdaLoggerOptions(configuration);
      var provider = new BunyanLambdaILoggerProvider(options);
      factory.AddProvider(provider);
      return factory;
    }

    /// <summary>
    /// Adds a Lambda logger provider with options loaded from the specified subsection of the
    /// configuration section.
    /// </summary>
    /// <param name="factory">ILoggerFactory to add Lambda logger to.</param>
    /// <param name="configuration">IConfiguration to use when construction logging options.</param>
    /// <param name="loggingSectionName">Name of the logging section with required settings.</param>
    /// <returns>Updated ILoggerFactory.</returns>
    [CLSCompliant(false)] // https://github.com/aspnet/Logging/issues/500
    public static ILoggerFactory AddBunyanLambdaLogger(this ILoggerFactory factory, IConfiguration configuration,
      string loggingSectionName)
    {
      if (factory == null)
      {
        throw new ArgumentNullException(nameof(factory));
      }
      if (configuration == null)
      {
        throw new ArgumentNullException(nameof(configuration));
      }
      if (string.IsNullOrEmpty(loggingSectionName))
      {
        throw new ArgumentNullException(nameof(loggingSectionName));
      }

      var options = new LambdaLoggerOptions(configuration, loggingSectionName);
      var provider = new BunyanLambdaILoggerProvider(options);
      factory.AddProvider(provider);
      return factory;
    }
  }
}