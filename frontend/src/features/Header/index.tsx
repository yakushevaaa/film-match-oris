import { useLocation, useNavigate } from "react-router-dom";
import logoIcon from "/icons/logo.svg";
import styles from "./index.module.scss";
import cn from "classnames";
import { NAV_CONFIG } from "./consts/linksList";
import { NavLinkCustom } from "@/shared/components/ui/NavLinkCustom";
import { useAuth } from "@/shared/lib/authProvider";
import { getUsernameFromToken } from "@/shared/lib/tokenUtils";

export const Header = () => {
  const location = useLocation();
  const { token } = useAuth();
  const username = getUsernameFromToken(token);
  const navigate = useNavigate();

  const matchedPath = Object.keys(NAV_CONFIG)
    .sort((a, b) => b.length - a.length)
    .find((path) => location.pathname.startsWith(path));
  const navLinks = matchedPath ? NAV_CONFIG[matchedPath] : [];

  return (
    <header
      className={cn(
        styles.header,
        location.pathname === "/" && styles.headerDarkBg
      )}
    >
      <div className={styles.header__info}>
      <img className={styles.header__logo} src={logoIcon} />

{navLinks.length > 0 && (
  <nav className={styles.nav}>
    <ul className={styles.nav__list}>
      {navLinks.map(({ label, to, color, isAccent }) => (
        <li key={to} className={styles.nav__item}>
          <NavLinkCustom
            to={to}
            color={(color ?? "dark") as "light" | "dark"}
            isAccent={isAccent}
            className={styles.nav__link}
          >
            {label}
          </NavLinkCustom>
        </li>
      ))}
    </ul>
  </nav>
)}
      </div>
     
      <div
        className={cn(
          styles.userBlock,
          location.pathname === "/" && styles.userBlockLight
        )}
        onClick={() => navigate(username ? "/profile" : "/auth/login")}
      >
        {username ? `Пользователь: ${username}` : "Войти"}
      </div>
    </header>
  );
};
