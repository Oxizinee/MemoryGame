mergeInto(LibraryManager.library,{
    
    DisplayInAlertWindow: function (messagePointer)
    {
        var message = Pointer_stringify(messagePointer);
       window.alert(message);
    }

    
}

);

mergeInto(LibraryManager.library,{
StringReturnValue : function(elementID)
    {
        var stringID = Pointer_stringify(elementID)
        var txtName = parent.document.getElementById(stringID);
        var returnString = txtName.value;
        var buffer = _malloc(returnString.length + 1);
        writeStringToMemory(returnString, buffer);
        return buffer;
    }
}
);