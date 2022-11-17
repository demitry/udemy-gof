using Coding.Exercise;

var cb = 
    new CodeBuilder("Person")
        .AddField("Name", "string")
        .AddField("Age", "int");

Console.WriteLine(cb);

namespace Coding.Exercise
{   
    class Field
    {
        private string PublicAccessModifier = "public";
        private string TwoSpaceIndentation = "  ";
        private string Name { get; set; }
        private string Type { get; set; }
        public Field(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
        public override string ToString() =>
            $"{TwoSpaceIndentation}{PublicAccessModifier} {Type} {Name};\n";
    }

    public class CodeBuilder
    {
        private string className;
        private List<Field> fields = new List<Field>();

        public CodeBuilder(string className)
        {
            this.className = className;
        }

        public CodeBuilder AddField(string fieldName, string fieldType)
        {
            fields.Add(new Field(fieldName, fieldType));
            return this;
        }

        private string GetClassHeader() => $"public class {className}\n";
        private string OpenBrace() => "{\n";
        private string CloseBrace() => "}\n";
        //private string GetFieldList() => fields.ConvertAll(x => x.ToString()).Aggregate((i, j) => i + j);
        private string GetFieldList() => string.Join("", fields); //KISS

        public override string ToString() =>
            string.Concat(
                GetClassHeader(),
                OpenBrace(),
                GetFieldList(),
                CloseBrace());
    }
}