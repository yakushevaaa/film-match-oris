import { useForm } from "react-hook-form";
import { Input, InputProps } from "@shared/components/ui/Input";
import { Checkbox } from "@shared/components/ui/CheckBox";
import { Button } from "@shared/components/ui/Button";
import { REGISTER_INPUTS, LOGIN_INPUTS } from "./consts/inputs";
import { FC, useState } from "react";
import styles from "./index.module.scss";
import { registerUser } from "./api/registerUserApi";
import { loginUser } from "./api/loginUserApi";
import { useNavigate } from "react-router-dom";
import { useAuth } from "@/shared/lib/authProvider";

interface FormProps {
  type: "register" | "login";
}

interface FormValues {
  name?: string;
  email?: string;
  password: string;
  confirmPassword?: string;
  rememberMe?: boolean;
}

export const AuthForm: FC<FormProps> = ({ type }) => {
  const isRegister = type === "register";
  const navigate = useNavigate();
  const { setToken } = useAuth();

  const [form, setForm] = useState<FormValues>({
    name: "",
    email: "",
    password: "",
    confirmPassword: "",
    rememberMe: false,
  });
  const [errors, setErrors] = useState<
    Partial<Record<keyof FormValues, string>>
  >({});
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, type: inputType, checked } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: inputType === "checkbox" ? checked : value,
    }));
  };

  const validate = (): boolean => {
    const newErrors: Partial<Record<keyof FormValues, string>> = {};
    if (isRegister && !form.name) newErrors.name = "Это поле обязательно";
    if (!form.email) newErrors.email = "Это поле обязательно";
    if (!form.password) newErrors.password = "Это поле обязательно";
    if (isRegister) {
      if (!form.confirmPassword) newErrors.confirmPassword = "Повторите пароль";
      if (form.password && form.password.length < 8)
        newErrors.password = "Минимум 8 символов";
      if (form.password && !/^(?=.*[!@#$%^&*])/.test(form.password))
        newErrors.password = "Пароль должен содержать хотя бы один спецсимвол";
      if (form.password !== form.confirmPassword)
        newErrors.confirmPassword = "Пароли не совпадают";
    }
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!validate()) return;
    try {
      if (isRegister) {
        const { name, email, password } = form;
        const errorMessage = await registerUser({
          name: name!,
          email: email!,
          password: password!,
        });
        if (errorMessage) {
          setErrors({ password: errorMessage });
          setSuccessMessage(null);
        } else {
          const loginError = await loginUser({ email: email!, password: password! });
          if (loginError) {
            setErrors({ password: loginError });
            setSuccessMessage(null);
          } else {
            setToken(localStorage.getItem("token"));
            setSuccessMessage("Успешно");
            setTimeout(() => navigate("/"), 1000);
          }
        }
      } else {
        const { email, password } = form;
        const errorMessage = await loginUser({
          email: email!,
          password: password!,
        });
        if (errorMessage) {
          setErrors({ password: errorMessage });
          setSuccessMessage(null);
        } else {
          setToken(localStorage.getItem("token"));
          setSuccessMessage("Успешно");
          setTimeout(() => navigate("/"), 1000);
        }
      }
    } catch (err) {
      setErrors({ password: "Неизвестная ошибка" });
      setSuccessMessage(null);
    }
  };

  return (
    <>
      <form className={styles.form} onSubmit={onSubmit}>
        {(isRegister ? REGISTER_INPUTS : LOGIN_INPUTS).map((input) => (
          <div key={input.label}>
            <Input
              name={input.label?.toLowerCase()}
              value={
                typeof form[input.label?.toLowerCase() as keyof FormValues] ===
                "string"
                  ? (form[
                      input.label?.toLowerCase() as keyof FormValues
                    ] as string)
                  : ""
              }
              onChange={handleChange}
              placeholder={input.placeholder}
              type={input.type}
              className={styles.passwordInput}
              label={input.label}
            />
            {errors[input.label?.toLowerCase() as keyof FormValues] && (
              <span className={styles.error}>
                {errors[input.label?.toLowerCase() as keyof FormValues]}
              </span>
            )}
          </div>
        ))}

        {isRegister && (
          <div className={styles.password_container}>
            <div>
              <Input
                name="password"
                type="password"
                placeholder="Password"
                className={styles.passwordInput}
                value={form.password}
                onChange={handleChange}
                label="Пароль"
              />
              {errors.password && (
                <span className={styles.error}>{errors.password}</span>
              )}
            </div>
            <div>
              <Input
                name="confirmPassword"
                type="password"
                placeholder="Repeat password"
                className={styles.passwordInput}
                value={form.confirmPassword}
                onChange={handleChange}
                label="Повторите пароль"
              />
              {errors.confirmPassword && (
                <span className={styles.error}>{errors.confirmPassword}</span>
              )}
            </div>
          </div>
        )}
        {!isRegister && (
          <div>
            <Input
              name="password"
              type="password"
              placeholder="Password"
              className={styles.passwordInput}
              value={form.password}
              onChange={handleChange}
              label="Пароль"
            />
            {errors.password && (
              <span className={styles.error}>{errors.password}</span>
            )}
          </div>
        )}

        <div className={styles.form__interactive_container}>
          <Checkbox
            label="Запомнить"
            checked={!!form.rememberMe}
            onChange={(e) =>
              setForm((prev) => ({ ...prev, rememberMe: e.target.checked }))
            }
            name="rememberMe"
          />
          <a className={styles.form__link} href="">
            Забыли пароль?
          </a>
        </div>

        {successMessage && (
          <div className={styles.form__success}>{successMessage}</div>
        )}

        <Button
          className={styles.form__button}
          appearance="accent"
          type="submit"
        >
          Продолжить
        </Button>
      </form>
    </>
  );
};
