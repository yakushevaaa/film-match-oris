import styles from "./index.module.scss";
import { Outlet } from "react-router-dom";

export const AuthLayout = () => {
  return (
    <div className={styles.auth_container}>
      <Outlet />
    </div>
  );
};
