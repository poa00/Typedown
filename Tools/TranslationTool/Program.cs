﻿// See https://aka.ms/new-console-template for more information
using TranslationTool;

var inputs = TextResource.ReadItems("C:\\Users\\12283\\Documents\\GitHub\\Typedown\\Dev\\Typedown.Core\\Resources\\Strings\\zh-Hans\\");
var dictionary = TextDictionary.ReadItems(@"C:\Users\12283\Documents\GitHub\Typedown\Tools\TranslationTool\Dictionary\");
dictionary = dictionary.Merge(inputs, "zh-Hans").ToList();
dictionary.WriteItems(@"C:\Users\12283\Documents\GitHub\Typedown\Tools\TranslationTool\Dictionary\");

Console.WriteLine("完成");