import {
  CommandInput,
  CommandEmpty,
  CommandGroup,
  CommandItem,
  Command,
} from "../ui/command";
import { Check, ChevronsUpDown } from "lucide-react";
import { Form } from "../ui/form";
import { Button } from "../ui/button";
import { UseFormReturn } from "react-hook-form";
import { z } from "zod";
import { Popover, PopoverTrigger, PopoverContent } from "../ui/popover";
import { ScrollArea } from "../ui/scrollarea";
import { FuelCardFields } from "./fields/fuelcard-fields";
import { cn } from "@/lib/utils";
import * as React from "react";

const frameworks = [
  {
    value: "next.js",
    label: "Next.js",
  },
  {
    value: "sveltekit",
    label: "SvelteKit",
  },
  {
    value: "nuxt.js",
    label: "Nuxt.js",
  },
  {
    value: "remix",
    label: "Remix",
  },
  {
    value: "astro",
    label: "Astro",
  },
];

type CreateFuelCardFormProps = {
  form: UseFormReturn<any>;
  formSchema: z.ZodObject<any, any>;
};

export function CreateFuelCardForm({
  form,
  formSchema,
}: CreateFuelCardFormProps) {
  const [step, setStep] = React.useState("Fuel");
  const [open, setOpen] = React.useState(false);
  const [value, setValue] = React.useState("");

  function onSubmit(values: z.infer<typeof formSchema>) {
    console.log(values);
  }

  return (
    <ScrollArea className="h-screen pr-7">
      <div className="m-1">
        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit)}
            className="flex-auto space-y-8 pb-20"
          >
            {step === "Fuel" && (
              <>
                <FuelCardFields control={form.control} />
                <Button onClick={() => setStep("Check")}>Next</Button>
              </>
            )}
            {step === "Check" && (
              <>
                {/* Voeg hier formulierelementen voor de "Check" stap toe */}
                <div className="border-l border-gray-300 mx-4" />
                <div>
                  <Popover open={open} onOpenChange={setOpen}>
                    <PopoverTrigger asChild>
                      <Button
                        variant="outline"
                        role="combobox"
                        aria-expanded={open}
                        className="w-[200px] justify-between"
                      >
                        {value
                          ? frameworks.find(
                              (framework) => framework.value === value
                            )?.label
                          : "Select framework..."}
                        <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                      </Button>
                    </PopoverTrigger>
                    <PopoverContent className="w-[200px] p-0">
                      <Command>
                        <CommandInput placeholder="Search framework..." />
                        <CommandEmpty>No framework found.</CommandEmpty>
                        <CommandGroup>
                          {frameworks.map((framework) => (
                            <CommandItem
                              key={framework.value}
                              value={framework.value}
                              onSelect={(currentValue) => {
                                setValue(
                                  currentValue === value ? "" : currentValue
                                );
                                setOpen(false);
                              }}
                            >
                              <Check
                                className={cn(
                                  "mr-2 h-4 w-4",
                                  value === framework.value
                                    ? "opacity-100"
                                    : "opacity-0"
                                )}
                              />
                              {framework.label}
                            </CommandItem>
                          ))}
                        </CommandGroup>
                      </Command>
                    </PopoverContent>
                  </Popover>
                </div>
                <Button onClick={() => setStep("Fuel")} className="mr-[10%]">
                  Previous
                </Button>
                <Button type="submit">Submit</Button>
              </>
            )}
          </form>
        </Form>
      </div>
    </ScrollArea>
  );
}
