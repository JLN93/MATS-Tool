-# MATS-Tool - </br>

MATS is a tool for Automating test generation and test selection for UPPAAL SMC.

![tablet-6092248_1280](https://user-images.githubusercontent.com/7644735/216753181-7c004949-8448-4f40-8278-81125c71007b.jpg)


This tool is currently tailored to work on the provided Brake by wire model and its manually mutated mutants. However MATS can be modified to support other models. The included models are provided.

In order to run the tool, you will first have to download UPPAAL SMC's command-line tool "verifyta.exe" (found inside UPPAAL bin-Win32)
and place it inside "\bin-win32" of the MATS-tool.

Testing is an essential process for ensuring the quality of the software. Designing software with as few errors as possible in most embedded systems is often critical. Resource usage is a significant concern for proper behaviour because of the very nature of embedded systems. To design energy-efficient systems, approaches are needed to catch desirable consumption points and correct them before deployment. Model-based testing can reduce testing effort, one testing method that allows for automatic test generation. However, this technique has yet to be studied extensively for revealing resource usage anomalies in embedded systems development. UPPAAL SMC is a statistical model-checking tool that can model the system’s resource usage. We show experimental results on automated test generation and selection using mutation analysis in UPPAAL SMC and how this is applied to a Brake by Wire industrial system. The evaluation shows that this approach is applicable and efficient for energy-based test generation. 
