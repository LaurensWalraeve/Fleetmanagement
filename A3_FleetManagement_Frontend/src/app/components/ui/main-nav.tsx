"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";

import { cn } from "@/lib/utils";

export function MainNav({
  className,
  ...props
}: React.HTMLAttributes<HTMLElement>) {
  const pathname = usePathname();

  const getLinkClass = (href: string) => {
    if (pathname === href) {
      return "text-sm font-bold text-primary cursor-default"; // Updated to show the current page more obviously and to change the cursor to default
    } else {
      return "text-sm font-medium text-muted-foreground transition-colors hover:text-primary";
    }
  };

  const renderLinkContent = (href: string, label: string) => {
    const linkClass = getLinkClass(href);
    if (pathname === href) {
      return <span className={linkClass}>{label}</span>; // Renders as a span for current page
    }
    return (
      <Link href={href} className={linkClass}>
        {label}
      </Link>
    );
  };

  return (
    <nav
      className={cn("flex items-center space-x-4 lg:space-x-6", className)}
      {...props}
    >
      {renderLinkContent("/dashboard", "Overview")}
      {renderLinkContent("/dashboard/driver", "Drivers")}
      {renderLinkContent("/dashboard/vehicle", "Vehicles")}
      {renderLinkContent("/dashboard/fuelcard", "Fuel Cards")}
      {/* {renderLinkContent("/settings", "Settings")} */}
    </nav>
  );
}
