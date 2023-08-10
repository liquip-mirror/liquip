# Threading

to use the threading lib you need to add

* Liquip.Threading
* Liquip.Threading.Plugs

and add this to your Kernel

<pre>
protected override void OnBoot()
{
    base.OnBoot();

    ProcessorScheduler.Initialize();
}
</pre>
