﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XUnity.AutoTranslator.Plugin.Core.Constants;

namespace XUnity.AutoTranslator.Plugin.Core.Configuration
{
   public static class Settings
   {
      // cannot be changed
      public static readonly int MaxErrors = 5;
      public static readonly float ClipboardDebounceTime = 1f;
      public static readonly int MaxTranslationsBeforeSlowdown = 1000;
      public static readonly int MaxTranslationsBeforeShutdown = 6000;
      public static readonly int MaxUnstartedJobs = 3500;

      public static int DefaultMaxConcurrentTranslations = 2;
      public static int MaxConcurrentTranslations = DefaultMaxConcurrentTranslations;
      public static bool IsShutdown = false;

      public static readonly float MaxTranslationsQueuedPerSecond = 5;
      public static readonly int MaxSecondsAboveTranslationThreshold = 30;
      public static readonly int TranslationQueueWatchWindow = 10;
      
      // can be changed
      public static string ServiceEndpoint;
      public static string Language;
      public static string FromLanguage;
      public static string OutputFile;
      public static string TranslationDirectory;
      public static float Delay;
      public static int MaxCharactersPerTranslation;
      public static bool EnablePrintHierarchy;
      public static string AutoTranslationsFilePath;
      public static bool EnableIMGUI;
      public static bool EnableUGUI;
      public static bool EnableNGUI;
      public static bool EnableTextMeshPro;
      public static bool AllowPluginHookOverride;
      public static bool IgnoreWhitespaceInDialogue;
      public static int MinDialogueChars;
      public static bool EnableSSL;
      public static string BaiduAppId;
      public static string BaiduAppSecret;
      public static int ForceSplitTextAfterCharacters;

      public static bool CopyToClipboard;
      public static int MaxClipboardCopyCharacters;

      public static void Configure()
      {
         try
         {
            // clear configuration from old versions...
            var section = Config.Current.Preferences[ "AutoTranslator" ];
            foreach( var key in section.Keys.ToList() )
            {
               section.DeleteKey( key.Key );
            }

            Config.Current.Preferences.DeleteSection( "AutoTranslator" );
         }
         catch( Exception e )
         {
            Console.WriteLine( "[XUnity.AutoTranslator][ERROR]: An error occurred while removing legacy configuration. " + Environment.NewLine + e );
         }



         ServiceEndpoint = Config.Current.Preferences[ "Service" ][ "Endpoint" ].GetOrDefault( KnownEndpointNames.GoogleTranslate, true );
         EnableSSL = Config.Current.Preferences[ "Service" ][ "EnableSSL" ].GetOrDefault( true);

         Language = Config.Current.Preferences[ "General" ][ "Language" ].GetOrDefault( "en" );
         FromLanguage = Config.Current.Preferences[ "General" ][ "FromLanguage" ].GetOrDefault( "ja", true );

         TranslationDirectory = Config.Current.Preferences[ "Files" ][ "Directory" ].GetOrDefault( @"Translation" );
         OutputFile = Config.Current.Preferences[ "Files" ][ "OutputFile" ].GetOrDefault( @"Translation\_AutoGeneratedTranslations.{lang}.txt" );

         EnableIMGUI = Config.Current.Preferences[ "TextFrameworks" ][ "EnableIMGUI" ].GetOrDefault( false );
         EnableUGUI = Config.Current.Preferences[ "TextFrameworks" ][ "EnableUGUI" ].GetOrDefault( true );
         EnableNGUI = Config.Current.Preferences[ "TextFrameworks" ][ "EnableNGUI" ].GetOrDefault( true );
         EnableTextMeshPro = Config.Current.Preferences[ "TextFrameworks" ][ "EnableTextMeshPro" ].GetOrDefault( true );
         AllowPluginHookOverride = Config.Current.Preferences[ "TextFrameworks" ][ "AllowPluginHookOverride" ].GetOrDefault( true );

         Delay = Config.Current.Preferences[ "Behaviour" ][ "Delay" ].GetOrDefault( 0f );
         MaxCharactersPerTranslation = Config.Current.Preferences[ "Behaviour" ][ "MaxCharactersPerTranslation" ].GetOrDefault( 150 );
         IgnoreWhitespaceInDialogue = Config.Current.Preferences[ "Behaviour" ][ "IgnoreWhitespaceInDialogue" ].GetOrDefault( true );
         MinDialogueChars = Config.Current.Preferences[ "Behaviour" ][ "MinDialogueChars" ].GetOrDefault( 20 );
         ForceSplitTextAfterCharacters = Config.Current.Preferences[ "Behaviour" ][ "ForceSplitTextAfterCharacters" ].GetOrDefault( 0 );
         CopyToClipboard = Config.Current.Preferences[ "Behaviour" ][ "CopyToClipboard" ].GetOrDefault( false );
         MaxClipboardCopyCharacters = Config.Current.Preferences[ "Behaviour" ][ "MaxClipboardCopyCharacters" ].GetOrDefault( 450 );

         BaiduAppId = Config.Current.Preferences[ "Baidu" ][ "BaiduAppId" ].GetOrDefault( "" );
         BaiduAppSecret = Config.Current.Preferences[ "Baidu" ][ "BaiduAppSecret" ].GetOrDefault( "" );
         
         EnablePrintHierarchy = Config.Current.Preferences[ "Debug" ][ "EnablePrintHierarchy" ].GetOrDefault( false );

         AutoTranslationsFilePath = Path.Combine( Config.Current.DataPath, OutputFile.Replace( "{lang}", Language ) );

         Config.Current.SaveConfig();
      }
   }
}
