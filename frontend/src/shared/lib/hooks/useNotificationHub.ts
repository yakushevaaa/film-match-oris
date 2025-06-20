import { useEffect, useRef } from "react";
import * as signalR from "@microsoft/signalr";

export const useNotificationHub = (
  userId: string | null,
  onNotification: (data: any) => void
) => {
  const connectionRef = useRef<signalR.HubConnection | null>(null);

  useEffect(() => {
    if (!userId) return;

    const connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5210/notificationHub", {
        accessTokenFactory: () => localStorage.getItem("token") || ""
      })
      .withAutomaticReconnect()
      .build();

    connectionRef.current = connection;

    connection
      .start()
      .then(() => {
        // Если на сервере реализовано разделение по группам
        connection.invoke("JoinGroup", userId).catch(console.error);
      })
      .catch(console.error);

    connection.on("ReceiveNotification", (data) => {
      onNotification(data);
    });

    return () => {
      connection.stop();
    };
  }, [userId, onNotification]);
}; 