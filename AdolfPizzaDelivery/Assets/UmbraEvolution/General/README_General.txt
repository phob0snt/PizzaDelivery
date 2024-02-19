Umbra Evolution General Scripts v.Infinity
------------------------------------------
Hey, if you're reading this, good on ya! You're very thorough. Also, have a wonderful day/evening :)

Chances are, you won't ever have to deal with anything in this folder, but it's here because any Attributes and their associated Property Drawers as well as any other random things I use/can use in multiple asset store tools (like my script that converts from Vectors to float arrays for example) reside here. While I could duplicate things like my ReadOnlyInInspector attribute and place a copy in every project, it's just cleaner to tuck them into one folder that comes with every asset store tool I post. The following is a list of what's in here along with a quick description:

1) ReadOnlyInInspector attribute: When added to any public variables, they will show in the inspector but be greyed out and you will not be able to set their values from the inspector. Usefull when you have a value you want exposed or serialized, but do not want messed with.

2) InvisibleInInspector attribute: The same as above, but the variable will not be visible at all.

3) GreaterThanFloat: An attribute that should be added only to variables of the float type. Needs a float to measure against and a bool passed in. If the bool is set to true, the variable will have it's inspector value clamped to greater than or equal to the provided value. If the bool is set to false, the variable will have its inspector value clamped to greater than the provided value (0.01f higher).

4) GreaterThanInt: The same as above, but should only be added to variables of the int or long types. It also takes an int and a bool instead of a float and a bool as parameters.

5) LessThanFloat: Again, the same as the above, but makes sure the value of the variable is less than (or less than or equal to) the specified value.

6) LessThanInt: I'm sure you get the idea at this point ;)

7) SerializeUnityThings: Converts Vectors and transforms to and from float arrays (usefull if you're running into a binary serialization problem because you're not using Unity's serialization or XML).

8) A bunch of editor code: There's a folder with editor code that holds the Property Drawers that go with the Attributes as well as any other editor code necessary to the general use scripts.

Feel free to use any of the above in your projects! If you want to use the attributes, remember you'll either have to use the format [UmbraEvolution.AttributeName] or add a using statement for the UmbraEvolution namespace.

Cheers!