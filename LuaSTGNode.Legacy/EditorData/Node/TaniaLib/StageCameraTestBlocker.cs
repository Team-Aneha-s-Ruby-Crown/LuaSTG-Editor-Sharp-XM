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
    [Serializable, NodeIcon("camerasetter.png")]
    [RequireAncestor(typeof(CodeAlikeTypes))]
    [LeafNode]
    class StageCameraTestBlocker : TreeNode
    {
        [JsonConstructor]
        private StageCameraTestBlocker() : base() { }

        public StageCameraTestBlocker(DocumentData workSpaceData)
            : base(workSpaceData) { }

        public override IEnumerable<string> ToLua(int spacing)
        {
            string sp = Indent(spacing);
            yield return sp + "task._Wait(10)\n"
                        + sp + "player.time_stop = true\n"
                        + sp + "New(camera_setter)\n"
                        + sp + "ex.WaitForSignal(\"Moyoshi\",1)\n";
        }

        public override string ToString()
        {
            return $"Stage Camera Test Blocker";
        }

        public override IEnumerable<Tuple<int, TreeNode>> GetLines()
        {
            yield return new Tuple<int, TreeNode>(1, this);
        }

        public override object Clone()
        {
            var n = new StageCameraTestBlocker(parentWorkSpace);
            n.DeepCopyFrom(this);
            return n;
        }
    }
}