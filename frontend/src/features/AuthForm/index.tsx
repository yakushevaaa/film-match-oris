import { Input, InputProps } from "@shared/components/ui/Input";
import { Checkbox } from "@shared/components/ui/CheckBox";
import { Button } from "@shared/components/ui/Button";
import { REGISTER_INPUTS, LOGIN_INPUTS } from "./consts/inputs";
import { FC } from "react";
import styles from "./index.module.scss";

interface FormProps {
  type: "register" | "login";
}

export const AuthForm: FC<FormProps> = ({ type }) => {
  const isRegister = type === "register";
  const inputs = isRegister ? REGISTER_INPUTS : LOGIN_INPUTS;

  // const passwordInputs = inputs.filter(
  //   (input) => input.type === "password" || input.id === "confirmPassword"
  // );

  return (
    <form className={styles.form} action="">
      {inputs.map((input) => (
        <Input key={input.id} {...input} />
      ))}

      {isRegister ? (
        <div className={styles.password_container}>
          <Input
            type="password"
            placeholder="Password"
            id="password"
            className={styles.passwordInput}
          />
          <Input
            type="password"
            placeholder="Repeat password"
            id=" password"
            className={styles.passwordInput}
          />
        </div>
      ) : (
        <>
          <Input
            type="password"
            placeholder="Password"
            id="password"
            className={styles.passwordInput}
          />
        </>
      )}

      <div className={styles.form__interactive_container}>
        <Checkbox label="Запомнить" value="rememberMe" />
        <a className={styles.form__link} href="">
          Забыли пароль?
        </a>
      </div>
      <Button className={styles.form__button} appearance="accent" type="submit">
        Продолжить
      </Button>
    </form>
  );
};
