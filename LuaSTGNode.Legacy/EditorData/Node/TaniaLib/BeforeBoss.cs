using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuaSTGEditorSharp.EditorData;
using LuaSTGEditorSharp.EditorData.Document;
using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;

namespace LuaSTGEditorSharp.EditorData.Node.Tania
{
    [Serializable, NodeIcon("beforeboss.png")]
    [RequireAncestor(typeof(CodeAlikeTypes))]
    [LeafNode]
    public class BeforeBoss : TreeNode
    {
        [JsonConstructor]
        private BeforeBoss() : base() { }

        public BeforeBoss(DocumentData workSpaceData)
            : base(workSpaceData) { }

        public override IEnumerable<string> ToLua(int spacing)
        {
            string sp = Indent(spacing);
            string s1 = Indent(1);
            yield return sp + "local _p1 = player\n"
            + sp + "New(bullet_killer, _p1.x, _p1.y, false)\n"
            + sp + "for _, unit in ObjList(GROUP_ENEMY) do\n"
            + sp + s1 + "Kill(unit)\n"
            + sp + "end\n"
            + sp + "for _, unit in ObjList(GROUP_NONTJT) do\n"
            + sp + s1 + "Kill(unit)\n"
            + sp + "end\n"
            + sp + "for _, unit in ObjList(GROUP_INDES) do\n"
            + sp + s1 + "Kill(unit)\n"
            + sp + "end\n";
        }

        public override IEnumerable<Tuple<int, TreeNode>> GetLines()
        {
            yield return new Tuple<int, TreeNode>(1, this);
        }

        public override string ToString()
        {
            return "Delete Enemies, non-TJT and Indes";
        }

        public override object Clone()
        {
            var n = new BeforeBoss(parentWorkSpace);
            n.DeepCopyFrom(this);
            return n;
        }
    }
}