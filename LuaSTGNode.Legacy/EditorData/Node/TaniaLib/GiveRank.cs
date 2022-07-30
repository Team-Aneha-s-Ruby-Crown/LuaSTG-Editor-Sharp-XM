using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuaSTGEditorSharp.EditorData.Message;
using LuaSTGEditorSharp.EditorData.Document;
using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;

namespace LuaSTGEditorSharp.EditorData.Node.Tania
{
    [Serializable, NodeIcon("giveRank.png")]
    [RequireAncestor(typeof(CodeAlikeTypes))]
    [LeafNode]
    [CreateInvoke(0), RCInvoke(1)]
    class GiveRank : TreeNode
    {
        [JsonConstructor]
        private GiveRank() : base() { }

        public GiveRank(DocumentData workSpaceData)
            : this(workSpaceData, "lstg.GetGlobal(\"PlayerDeathNumberOnStage\")", "lstg.GetGlobal(\"PlayerBombNumberOnStage\")", "6", "Hard") { }

        public GiveRank(DocumentData workSpaceData, string lifelost, string bomblost, string stage, string diffname)
            : base(workSpaceData)
        {
            Lifelost = lifelost;
            Bomblost = bomblost;
            Stage = stage;
            Diffname = diffname;
        }

        [JsonIgnore, NodeAttribute]
        public string Lifelost
        {
            get => DoubleCheckAttr(0).attrInput;
            set => DoubleCheckAttr(0).attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string Bomblost
        {
            get => DoubleCheckAttr(1).attrInput;
            set => DoubleCheckAttr(1).attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string Stage
        {
            get => DoubleCheckAttr(2).attrInput;
            set => DoubleCheckAttr(2).attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string Diffname
        {
            get => DoubleCheckAttr(3, "stageGroup", "Difficulty").attrInput;
            set => DoubleCheckAttr(3, "stageGroup", "Difficulty").attrInput = value;
        }

        public override IEnumerable<string> ToLua(int spacing)
        {
            string sp = Indent(spacing);
            yield return sp + $"giveRank({Macrolize(0)},{Macrolize(1)},{Macrolize(2)},{Macrolize(3)})\n";
        }

        public override string ToString()
        {
            return $"Give player rank for stage {NonMacrolize(2)} for difficulty {NonMacrolize(3)}";
        }

        public override IEnumerable<Tuple<int, TreeNode>> GetLines()
        {
            yield return new Tuple<int, TreeNode>(1, this);
        }

        public override object Clone()
        {
            var n = new GiveRank(parentWorkSpace);
            n.DeepCopyFrom(this);
            return n;
        }
    }
}