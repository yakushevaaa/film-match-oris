import { Header } from "@features/Header";
import { Footer } from "@features/Footer";
import { Outlet } from "react-router-dom";
import styles from "./index.module.scss";

export const MainLayout = () => {
  return (
    <div className={styles.layoutRoot}>
      <Header />
      <main className={styles.main}>
        <Outlet />
      </main>
      <Footer />
    </div>
  );
};
