function redirect(url)
{
    window.location = url;
}

function navTree(node)
{
    var me = this;
    var navTree;
    var selectedNode = node;
    var selectNode;
    
    YAHOO.util.Event.onContentReady("navTree", init);
    
    //YAHOO.util.Event.onContentReady(selectNode, selectedNode);
    /* Tree declaration */
    function init()
    {
        navTree = new YAHOO.widget.TreeView("navTree");
        //easyTree = new YAHOO.widget.TreeView("easyTree");
        
        //custom icon behaviour. node must have className=oldStyle or else it will not be replaced 
        navTree.subscribe("collapse", replaceLabelStyle, {newStyle: "folderClosed", oldStyle: "folderOpen"});
        navTree.subscribe("expand", replaceLabelStyle, {newStyle: "folderOpen", oldStyle: "folderClosed"});
        
        //YAHOO.util.Event.onAvailable(navTree.id, );
        declareTree();
        drawTree();
        selectNode(selectedNode);
    }
    /* Nodes declaration */
    
    function declareTree()
    {
        //top level nodes
        var root = navTree.getRoot();
        
        //reusable temp nodes
        var folder1; 
        var folder2;
        var folder3;
        var child1;
        var child2;
        
        //syntax: MYNODE = new YAHOO.widget.NODETYPE( {configurations}, parentNode, expand );
        folder1 = new YAHOO.widget.MenuNode({label: "Home", id: "home", href: "home.html", style: "config"}, root, false);
        folder1 = new YAHOO.widget.MenuNode({label:"EasyConfig", id: "easy_config", href: "easy_config.html" , style: "config"}, root, false);
        folder1 = new YAHOO.widget.MenuNode({label:"DeviceManager", id: "device_manager", href: "device_manager.html" , style: "config"}, root, false);
        folder1 = new YAHOO.widget.MenuNode({label:"TruePort", id: "trueport", href: "trueport.html", style: "config"}, root, false);
        folder1 = new YAHOO.widget.MenuNode({label: "Product Files", id: "product_files", href: "product_files.html", style: "config"}, root, false);
        child1 = new YAHOO.widget.TextNode({label:"DS", id: "ds", href: "ds.html" , style: "configItem"}, folder1, false);
        child1 = new YAHOO.widget.TextNode({label:"TS", id: "ts", href: "ts.html" , style: "configItem"}, folder1, false);
        child1 = new YAHOO.widget.TextNode({label:"SDS", id: "sds", href: "sds.html" , style: "configItem"}, folder1, false);
        child1 = new YAHOO.widget.TextNode({label:"SCS", id: "scs", href: "scs.html" , style: "configItem"}, folder1, false);
        child1 = new YAHOO.widget.TextNode({label:"STS", id: "sts", href: "sts.html" , style: "configItem"}, folder1, false);
        child1 = new YAHOO.widget.TextNode({label:"MDC", id: "mdc", href: "mdc.html" , style: "configItem"}, folder1, false);
        folder1 = new YAHOO.widget.MenuNode({label:"Utilities", id: "util", href: "util.html" , style: "config"}, root, false);
        folder1 = new YAHOO.widget.MenuNode({label: "Support", id: "support", href: "support.html", style: "config"}, root, false);
    } // end function declareTree()
    //end of node list

    /* Tree functions */
    
    //renders tree and applies styles to top level nodes
    function drawTree()
    {
        navTree.draw();
        var rootNode = navTree.getRoot();
        var div;
        //for each top level node fix the background
        for(var i=0; i< rootNode.children.length; i++)
        {
            div = rootNode.children[i].getEl();
            div.style.background = "url(images/navTree/nodeBG/mainNode.gif) no-repeat";
        }
    }
    
    selectNode = function(node)
    {
        var tmpNode = navTree.getNodeByProperty("id", node);
        var rootNode = navTree.getRoot();
        
        /*
        selected nodes should have a distingusihed style; however, top level nodes
        such as config, stats, etc.. need to be kept different 
        */
        
        var div = tmpNode.getEl();
        var a;
        //div will only be null if renderHidden in treeview.js is set to disable by default or the tree is not done rendering
        if(div != null) 
        {
            a = div.getElementsByTagName("a")[0];
            
            //top level nodes. make font bold
            if(tmpNode.getAncestor(this.depth) == rootNode)
            {
                a.style.fontWeight = 'bold';
            }
            //other nodes. highlight background color
            else
            {
                div.firstChild.style.backgroundColor = '#1a4cb3';
                a.style.color = 'white';
            }
        }
        //expand each parent element
        while(tmpNode != rootNode)
        {
            tmpNode.expand();
            tmpNode = tmpNode.getAncestor(this.depth-1);
        }
    }
    
    /* Custom behaviour */
    
    function replaceLabelStyle(node, styles)
    {
        if(YAHOO.util.Dom.hasClass(node.labelElId, styles.oldStyle))
        {
            YAHOO.util.Dom.replaceClass(node.labelElId, styles.oldStyle, styles.newStyle);
        }
    }
};
