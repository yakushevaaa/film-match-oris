import { useLocation } from "react-router-dom";
import logoIcon from "/icons/logo.svg";
import styles from "./index.module.scss";
import cn from "classnames";
import { NAV_CONFIG } from "./consts/linksList";
import { NavLinkCustom } from "@/shared/components/ui/NavLinkCustom";

export const Header = () => {
  const location = useLocation();

  const matchedPath = Object.keys(NAV_CONFIG).find((path) =>
    location.pathname.startsWith(path)
  );

  const navLinks = matchedPath ? NAV_CONFIG[matchedPath] : [];

  return (
    <header
      className={cn(
        styles.header,
        location.pathname === "/" && styles.headerDarkBg
      )}
    >
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
    </header>
  );
};
