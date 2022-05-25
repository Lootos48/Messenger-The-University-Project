MESSENGER SERVER JSON RESPONSES:
    
    UserDTO:
        "Id" | int
        "username" | string
        "password" | string
        "avatar" | byte-array | nullable
        "chats" | ChatDTO-array
        "messages" | MessageDTO-array
        
    ChatsDTO:
        "id" | int
        "title" | string
        "users" | UserDTO-array
        "messages" | MessageDTO-array
        
    MessageDTO:
        "id" | int
        "sendtime" | DateTime
        "text" | string
        "image" | byte-array | nullable 
        "userId" | int 
        "username" | string
        "useravatar" | byte-array | nullable
        "chatId" | int

MESSENGER SERVER ACTIONS:
    
    USER REGISTER:
        Link: http://localhost:4000/users/register
        Request:
            Type: POST
            JSON:
                "username" | string | required | 20 characters max
                "password" | string | required
        Response:
            if (success)
                status-code : 200
                JSON : "userId" | int
            if (invalid-request-data)
                status-code : 400
                JSON : "errors" | object-array
                    "request-field" | string-array of errors
            if (not-unique-username)
                status-code : 400
                JSON : "error" | string
        
    USER LOGIN:
        Link: http://localhost:4000/users/login
        Request:
            Type: POST
            JSON:
                "username" | string | required
                "password" | string | required
        Response:
            if (success)
                status-code : 200
                JSON : "userId" | int
            if (invalid-request-data)
                status-code : 400
                JSON : "errors" | object-array
                    "request-field" | string-array of errors
            if (user-not-found)
                status-code : 404
                JSON : "error" | string
                
    GET USER BY USERNAME
        Link: http://localhost:4000/users/{username}
        Request:
            Type: GET
            Value:
                "username" | string
        Response:
            if (success)
                status-code : 200
                JSON : UserDTO
            if (user-not-found)
                status-code : 404
                JSON : "error" | string
                
                
    GET USER BY USER ID
        Link: http://localhost:4000/users/{userId}
        Request:
            Type: GET
            Value:
                "userId" | int
        Response:
            if (success)
                status-code : 200
                JSON : UserDTO
            if (user-not-found)
                status-code : 404
                JSON : "error" | string
                
    
    GET ALL USERS
        Link: http://localhost:4000/users
        Request:
            Type: GET
        Response:
            if (success)
                status-code : 200
                JSON : UserDTO-array
                
                            
    GET USER CHATS BY USER ID
        Link: http://localhost:4000/users/{userId}/chats
        Request:
            Type: GET
            Value:
                "userId" | int
        Response:
            if (success)
                status-code : 200
                JSON : ChatDTO-array
    
                            
    EDIT USER
        Link: http://localhost:4000/users/edit
        Request:
            Type: POST
            JSON:
                "id" | int | required
                "username" | string | required | max length 20 characters
                "password" | string | required |must be equal to "confirmPassword"
                "confirmPassword" | string
        Response:
            if (success)
                status-code : 200
            if (user-not-found)
                status-code : 404
                JSON :
                    "error" | string
            if (username-is-not-unique)
                status-code : 400
                JSON :
                    "error" | string
            if (invalid-request-data)
                status-code : 400
                JSON : "errors" | object-array
                    "request-field" | string-array of errors
    
     DELETE USER
        Link: http://localhost:4000/users/delete?={userId}
        Request:
            Type: POST
            VALUE:
                "userId" | int
        Response:
            if (success)
                status-code : 200
            if (user-not-found)
                status-code : 404
                JSON :
                    "error" | string
                    
    GET ALL CHATS
        Link: http://localhost:4000/chats
        Request:
            Type: GET
        Response:
            if (success)
                status-code : 200
                JSON : ChatDTO-array
                
                
    GET CHAT BY CHAT ID
        Link: http://localhost:4000/chats/{chatId}
        Request:
            Type: GET
            Value:
                "chatId" | int
        Response:
            if (success)
                status-code : 200
                JSON : ChatDTO
            if (chat-not-found)
                status-code : 404
                JSON : "error" | string
                            
    GET CHAT USERS BY CHAT ID
        Link: http://localhost:4000/chats/{chatId}/users
        Request:
            Type: GET
            Value:
                "chatId" | int
        Response:
            if (success)
                status-code : 200
                JSON : UserDTO-array
                
    GET CHAT MESSAGES BY CHAT ID
        Link: http://localhost:4000/chats/{chatId}/messages
        Request:
            Type: GET
            Value:
                "chatId" | int
        Response:
            if (success)
                status-code : 200
                JSON : MessageDTO-array
                
    ADD USER TO CHAT
        Link: http://localhost:4000/chats/add-user
        Request:
            Type: POST
            JSON:
                "chatId" | int
                "userId" | int
        Response:
            if (success)
                status-code : 200
                
    REMOVE USER TO CHAT
        Link: http://localhost:4000/chats/remove-user
        Request:
            Type: POST
            JSON:
                "chatId" | int
                "userId" | int
        Response:
            if (success)
                status-code : 200
                
    CREATE CHAT
        Link: http://localhost:4000/chats/create
        Request:
            Type: POST
            JSON:
                "title" | string | required | max string length 100 characters
                "creatorId" | int | required |
        Response:
            if (success)
                status-code : 200
                JSON :
                    "chatId" | int
            if (title-is-not-unique)
                status-code : 400
                JSON :
                    "error" | string
            if (invalid-request-data)
                status-code : 400
                JSON : "errors" | object-array
                    "request-field" | string-array of errors
                    
    EDIT CHAT
        Link: http://localhost:4000/chats/edit
        Request:
            Type: POST
            JSON:
                "id" | int | required |
                "title" | string | required | max string length 100 characters
        Response:
            if (success)
                status-code : 200
                JSON :
                    "chatId" | int
            if (not-found)
                status-code : 404
                JSON :
                    "error" | string
            if (title-is-not-unique)
                status-code : 400
                JSON :
                    "error" | string
            if (invalid-request-data)
                status-code : 400
                JSON : "errors" | object-array
                    "request-field" | string-array of errors
                    
     DELETE CHAT
        Link: http://localhost:4000/chats/delete?={chatId}
        Request:
            Type: POST
            VALUE:
                "chatId" | int
        Response:
            if (success)
                status-code : 200
            if (user-not-found)
                status-code : 404
                JSON :
                    "error" | string
                    
    GET ALL MESSAGES
        Link: http://localhost:4000/messages
        Request:
            Type: GET
        Response:
            if (success)
                status-code : 200
                JSON : MessageDTO-array
                
    GET MESSAGE BY MESSAGE ID
        Link: http://localhost:4000/messages/{messageId}
        Request:
            Type: GET
            Value:
                "messageId" | int
        Response:
            if (success)
                status-code : 200
                JSON : MessageDTO
            if (chat-not-found)
                status-code : 404
                JSON : "error" | string               
                
    CREATE MESSAGE
        Link: http://localhost:4000/message/create
        Request:
            Type: POST
            JSON:
                "text" | string |
                "ImageBytes" | byte-array | nullable
                "userId" | int | required
                "chatId" | int | required
        Response:
            if (success)
                status-code : 200
                JSON :
                    "chatId" | int
            if (invalid-request-data)
                status-code : 400
                JSON : "errors" | object-array
                    "request-field" | string-array of errors
                      
    EDIT MESSAGE
        Link: http://localhost:4000/messages/edit
        Request:
            Type: POST
            JSON:
                "id" | int | required |
                "text" | string | required
                "imageBytes" | byte-array | nullable
        Response:
            if (success)
                status-code : 200
                JSON :
                    "chatId" | int
            if (not-found)
                status-code : 404
                JSON :
                    "error" | string
            
     DELETE MESSAGE
        Link: http://localhost:4000/messages/delete?={messageId}
        Request:
            Type: POST
            VALUE:
                "messageId" | int
        Response:
            if (success)
                status-code : 200
            if (user-not-found)
                status-code : 404
                JSON :
                    "error" | string
