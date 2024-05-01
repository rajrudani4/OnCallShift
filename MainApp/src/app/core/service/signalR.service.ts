import { Injectable } from '@angular/core';
import * as SignalR from '@microsoft/signalr';
import { environment } from "src/environments/environment";


@Injectable({ providedIn: 'root' })
export class SignalrService {

    constructor() {
    }

    hubConnection: signalR.HubConnection;

    startConnection = (username: string) => {

        //make connection
        this.hubConnection = new SignalR.HubConnectionBuilder()
            .withUrl(environment.hostUrl + 'toastr', {
                skipNegotiation: true,
                transport: SignalR.HttpTransportType.WebSockets   //to avoid cors issues
            })
            .build();

        //start connection
        this.hubConnection
            .start()
            .then(() => {

                //create and store connection
                this.hubConnection.invoke("saveConnection", username).then(value => {
                })
            })
            .catch(err => {
                console.log('Error while connecting with hub')
            });
    }

    //close connection when user logout
    closeConnection(username: string) {
        if (this.hubConnection) {
            this.hubConnection.invoke('closeConnection', username)
                .catch(err => console.log(err));
        }
    }

    // ------------- simple chat ------------------ 

    //mark all msgs seen where msgFrom is sender & msgTo is receiver
    seenMessages(sender: string, receiver: string) {
        this.hubConnection.invoke('seenMessages', sender, receiver);

        //FOR SENDER & RECEIVER UPDATE SEEN CNT
        this.hubConnection.invoke('GetRecentChat', receiver, sender);  //because params are ultaaaa
    }

    // --------- for profile status ---------
    updateProfileStatus(status: string, username: string) {
        this.hubConnection.invoke("UpdateProfileStatus", status, username)
            .catch(err => console.log(err));
    }
}