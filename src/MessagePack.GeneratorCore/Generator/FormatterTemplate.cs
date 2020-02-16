﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace MessagePackCompiler.Generator
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class FormatterTemplate : FormatterTemplateBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1200 // Using directives should be placed correctly
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            this.Write("\r\n{\r\n    using System;\r\n    using System.Buffers;\r\n    using MessagePack;\r\n");
 foreach(var objInfo in ObjectSerializationInfos) { 
            this.Write("\r\n    public sealed class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));
            this.Write("Formatter");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.TemplateParametersString != null? objInfo.TemplateParametersString : ""));
            this.Write(" : global::MessagePack.Formatters.IMessagePackFormatter<");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.FullName));
            this.Write(">\r\n    {\r\n");
 foreach(var item in objInfo.Members) { 
 if(item.CustomFormatterTypeName != null) { 
            this.Write("        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.CustomFormatterTypeName));
            this.Write(" __");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("CustomFormatter__ = new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.CustomFormatterTypeName));
            this.Write("();\r\n");
 } 
 } 
            this.Write("\r\n");
 if( objInfo.IsStringKey) { 
            this.Write("\r\n        private readonly global::MessagePack.Internal.AutomataDictionary ____ke" +
                    "yMapping;\r\n        private readonly byte[][] ____stringByteKeys;\r\n\r\n        publ" +
                    "ic ");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));
            this.Write("Formatter()\r\n        {\r\n            this.____keyMapping = new global::MessagePack" +
                    ".Internal.AutomataDictionary()\r\n            {\r\n");
 foreach(var x in objInfo.Members) { 
            this.Write("                { \"");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.StringKey));
            this.Write("\", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.IntKey));
            this.Write(" },\r\n");
 } 
            this.Write("            };\r\n\r\n            this.____stringByteKeys = new byte[][]\r\n           " +
                    " {\r\n");
 foreach(var x in objInfo.Members.Where(x => x.IsReadable)) { 
            this.Write("                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringBytes" +
                    "(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.StringKey));
            this.Write("\"),\r\n");
 } 
            this.Write("            };\r\n        }\r\n");
 } 
            this.Write("\r\n        public void Serialize(ref MessagePackWriter writer, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.FullName));
            this.Write(" value, global::MessagePack.MessagePackSerializerOptions options)\r\n        {\r\n");
 if( objInfo.IsClass) { 
            this.Write("            if (value == null)\r\n            {\r\n                writer.WriteNil();" +
                    "\r\n                return;\r\n            }\r\n\r\n");
 } 
            this.Write("            IFormatterResolver formatterResolver = options.Resolver;\r\n");
if(objInfo.HasIMessagePackSerializationCallbackReceiver && objInfo.NeedsCastOnBefore) { 
            this.Write("            ((IMessagePackSerializationCallbackReceiver)value).OnBeforeSerialize(" +
                    ");\r\n");
 } else if(objInfo.HasIMessagePackSerializationCallbackReceiver) { 
            this.Write("            value.OnBeforeSerialize();\r\n");
 } 
 if( objInfo.IsIntKey) { 
            this.Write("            writer.WriteArrayHeader(");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.MaxKey + 1));
            this.Write(");\r\n");
 } else { 
            this.Write("            writer.WriteMapHeader(");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.WriteCount));
            this.Write(");\r\n");
 } 
 if(objInfo.IsIntKey) { 
 for(var i =0; i<= objInfo.MaxKey; i++) { var member = objInfo.GetMember(i); 
 if( member == null) { 
            this.Write("            writer.WriteNil();\r\n");
 } else { 
            this.Write("            ");
            this.Write(this.ToStringHelper.ToStringWithCulture(member.GetSerializeMethodString()));
            this.Write(";\r\n");
 } } } else { 
 var index = 0; foreach(var x in objInfo.Members) { 
            this.Write("            writer.WriteRaw(this.____stringByteKeys[");
            this.Write(this.ToStringHelper.ToStringWithCulture(index++));
            this.Write("]);\r\n            ");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.GetSerializeMethodString()));
            this.Write(";\r\n");
 } } 
            this.Write("        }\r\n\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.FullName));
            this.Write(" Deserialize(ref MessagePackReader reader, global::MessagePack.MessagePackSeriali" +
                    "zerOptions options)\r\n        {\r\n            if (reader.TryReadNil())\r\n          " +
                    "  {\r\n");
 if( objInfo.IsClass) { 
            this.Write("                return null;\r\n");
 } else { 
            this.Write("                throw new InvalidOperationException(\"typecode is null, struct not" +
                    " supported\");\r\n");
 } 
            this.Write("            }\r\n\r\n            options.Security.DepthStep(ref reader);\r\n           " +
                    " IFormatterResolver formatterResolver = options.Resolver;\r\n");
 if(objInfo.IsStringKey) { 
            this.Write("            var length = reader.ReadMapHeader();\r\n");
 } else { 
            this.Write("            var length = reader.ReadArrayHeader();\r\n");
 } 
 foreach(var x in objInfo.Members) { 
            this.Write("            var __");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));
            this.Write("__ = default(");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Type));
            this.Write(");\r\n");
 } 
            this.Write("\r\n            for (int i = 0; i < length; i++)\r\n            {\r\n");
 if(objInfo.IsStringKey) { 
            this.Write(@"                ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                int key;
                if (!this.____keyMapping.TryGetValue(stringKey, out key))
                {
                    reader.Skip();
                    continue;
                }
");
 } else { 
            this.Write("                var key = i;\r\n");
 } 
            this.Write("\r\n                switch (key)\r\n                {\r\n");
 foreach(var x in objInfo.Members) { 
            this.Write("                    case ");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.IntKey));
            this.Write(":\r\n                        __");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));
            this.Write("__ = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.GetDeserializeMethodString()));
            this.Write(";\r\n                        break;\r\n");
 } 
            this.Write("                    default:\r\n                        reader.Skip();\r\n           " +
                    "             break;\r\n                }\r\n            }\r\n\r\n            var ____res" +
                    "ult = new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.GetConstructorString()));
            this.Write(";\r\n");
 foreach(var x in objInfo.Members.Where(x => x.IsWritable)) { 
            this.Write("            ____result.");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));
            this.Write(" = __");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));
            this.Write("__;\r\n");
 } 
if(objInfo.HasIMessagePackSerializationCallbackReceiver && objInfo.NeedsCastOnAfter) { 
            this.Write("            ((IMessagePackSerializationCallbackReceiver)____result).OnAfterDeseri" +
                    "alize();\r\n");
 } else if(objInfo.HasIMessagePackSerializationCallbackReceiver) { 
            this.Write("            ____result.OnAfterDeserialize();\r\n");
 } 
            this.Write("            reader.Depth--;\r\n            return ____result;\r\n        }\r\n    }\r\n");
 } 
            this.Write(@"}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1129 // Do not use default value type constructor
#pragma warning restore SA1200 // Using directives should be placed correctly
#pragma warning restore SA1309 // Field names should not begin with underscore
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name
");
            return this.GenerationEnvironment.ToString();
        }
    }
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class FormatterTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
