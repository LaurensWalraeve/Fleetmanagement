import "./globals.css";

import { Metadata } from "next";
import Image from "next/image";
import Link from "next/link";

import { cn } from "@/lib/utils";
import { buttonVariants } from "@/app/components/ui/button";
import { UserAuthForm } from "@/app/components/ui/user-auth-form";

export const metadata: Metadata = {
  title: "A3 Fleet App - Login",
  description: "Login page for A3 Fleet App.",
};

export default function AuthenticationPage() {
  return (
    <div className="h-screen flex flex-col justify-center items-center overflow-y-hidden">
      <div className="mx-auto flex w-full flex-col justify-center space-y-6 sm:w-[350px]">
        <div className="flex flex-col space-y-2 text-center">
          <h1 className="text-2xl font-semibold tracking-tight whitespace-nowrap">
            Enter your credentials to continue
          </h1>
          <p className="text-sm text-muted-foreground">
            Contact support if you have any issues logging in.
          </p>
        </div>
        {/* <Link href="/dashboard/user">
          <UserAuthForm />
        </Link> */}
        <Link href="/dashboard">
          <UserAuthForm />
        </Link>
        <p className="px-8 text-center text-sm text-muted-foreground">
          By clicking continue, you agree to our{" "}
          <Link
            href="/terms"
            className="underline underline-offset-4 hover:text-primary"
          >
            Terms of Service
          </Link>{" "}
          and{" "}
          <Link
            href="/privacy"
            className="underline underline-offset-4 hover:text-primary"
          >
            Privacy Policy
          </Link>
          .
        </p>
      </div>
    </div>
  );
}
