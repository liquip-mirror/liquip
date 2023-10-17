namespace Liquip.WASM.Interfaces;

/// <summary>
/// WebAssembly System Interface
/// </summary>
public interface IWasi
{

    /// <summary>
    /// args_get(argv: Pointer<Pointer<u8>>, argv_buf: Pointer<u8>) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] args_get(WasmValue[] args);

    /// <summary>
    /// args_sizes_get() -> Result<(size, size), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] args_sizes_get(WasmValue[] args);

    /// <summary>
    /// environ_get(environ: Pointer<Pointer<u8>>, environ_buf: Pointer<u8>) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] environ_get(WasmValue[] args);

    /// <summary>
    /// environ_sizes_get() -> Result<(size, size), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] environ_sizes_get(WasmValue[] args);

    /// <summary>
    /// clock_res_get(id: clockid) -> Result<timestamp, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] clock_res_get(WasmValue[] args);

    /// <summary>
    /// clock_time_get(id: clockid, precision: timestamp) -> Result<timestamp, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] clock_time_get(WasmValue[] args);

    /// <summary>
    /// fd_advise(fd: fd, offset: filesize, len: filesize, advice: advice) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_advise(WasmValue[] args);

    /// <summary>
    /// fd_allocate(fd: fd, offset: filesize, len: filesize) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_allocate(WasmValue[] args);

    /// <summary>
    /// fd_close(fd: fd) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_close(WasmValue[] args);

    /// <summary>
    /// fd_datasync(fd: fd) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_datasync(WasmValue[] args);

    /// <summary>
    /// fd_fdstat_get(fd: fd) -> Result<fdstat, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_fdstat_get(WasmValue[] args);

    /// <summary>
    /// fd_fdstat_set_flags(fd: fd, flags: fdflags) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_fdstat_set_flags(WasmValue[] args);

    /// <summary>
    /// fd_fdstat_set_rights(fd: fd, fs_rights_base: rights, fs_rights_inheriting: rights) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_fdstat_set_rights(WasmValue[] args);

    /// <summary>
    /// fd_filestat_get(fd: fd) -> Result<filestat, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_filestat_get(WasmValue[] args);

    /// <summary>
    /// fd_filestat_set_size(fd: fd, size: filesize) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_filestat_set_size(WasmValue[] args);

    /// <summary>
    /// fd_filestat_set_times(fd: fd, atim: timestamp, mtim: timestamp, fst_flags: fstflags) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_filestat_set_times(WasmValue[] args);

    /// <summary>
    /// fd_pread(fd: fd, iovs: iovec_array, offset: filesize) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_pread(WasmValue[] args);

    /// <summary>
    /// fd_prestat_get(fd: fd) -> Result<prestat, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_prestat_get(WasmValue[] args);

    /// <summary>
    /// fd_prestat_dir_name(fd: fd, path: Pointer<u8>, path_len: size) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_prestat_dir_name(WasmValue[] args);

    /// <summary>
    /// fd_pwrite(fd: fd, iovs: ciovec_array, offset: filesize) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_pwrite(WasmValue[] args);

    /// <summary>
    /// fd_read(fd: fd, iovs: iovec_array) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_read(WasmValue[] args);

    /// <summary>
    /// fd_readdir(fd: fd, buf: Pointer<u8>, buf_len: size, cookie: dircookie) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_readdir(WasmValue[] args);

    /// <summary>
    /// fd_renumber(fd: fd, to: fd) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_renumber(WasmValue[] args);

    /// <summary>
    /// fd_seek(fd: fd, offset: filedelta, whence: whence) -> Result<filesize, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_seek(WasmValue[] args);

    /// <summary>
    /// fd_sync(fd: fd) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_sync(WasmValue[] args);

    /// <summary>
    /// fd_tell(fd: fd) -> Result<filesize, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_tell(WasmValue[] args);

    /// <summary>
    /// fd_write(fd: fd, iovs: ciovec_array) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] fd_write(WasmValue[] args);

    /// <summary>
    /// path_create_directory(fd: fd, path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] path_create_directory(WasmValue[] args);

    /// <summary>
    /// path_filestat_get(fd: fd, flags: lookupflags, path: string) -> Result<filestat, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] path_filestat_get(WasmValue[] args);

    /// <summary>
    /// path_filestat_set_times(fd: fd, flags: lookupflags, path: string, atim: timestamp, mtim: timestamp, fst_flags: fstflags) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] path_filestat_set_times(WasmValue[] args);

    /// <summary>
    /// path_link(old_fd: fd, old_flags: lookupflags, old_path: string, new_fd: fd, new_path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] path_link(WasmValue[] args);

    /// <summary>
    /// path_open(fd: fd, dirflags: lookupflags, path: string, oflags: oflags, fs_rights_base: rights, fs_rights_inheriting: rights, fdflags: fdflags) -> Result<fd, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] path_open(WasmValue[] args);

    /// <summary>
    /// path_readlink(fd: fd, path: string, buf: Pointer<u8>, buf_len: size) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] path_readlink(WasmValue[] args);

    /// <summary>
    /// path_remove_directory(fd: fd, path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] path_remove_directory(WasmValue[] args);

    /// <summary>
    /// path_rename(fd: fd, old_path: string, new_fd: fd, new_path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] path_rename(WasmValue[] args);

    /// <summary>
    /// path_symlink(old_path: string, fd: fd, new_path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] path_symlink(WasmValue[] args);

    /// <summary>
    /// path_unlink_file(fd: fd, path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] path_unlink_file(WasmValue[] args);

    /// <summary>
    /// poll_oneoff(in: ConstPointer<subscription>, out: Pointer<event>, nsubscriptions: size) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] poll_oneoff(WasmValue[] args);

    /// <summary>
    /// proc_exit(rval: exitcode)
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] proc_exit(WasmValue[] args);

    /// <summary>
    /// proc_raise(sig: signal) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] proc_raise(WasmValue[] args);

    /// <summary>
    /// sched_yield() -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] sched_yield(WasmValue[] args);

    /// <summary>
    /// random_get(buf: Pointer<u8>, buf_len: size) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] random_get(WasmValue[] args);

    /// <summary>
    /// sock_accept(fd: fd, flags: fdflags) -> Result<fd, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] sock_accept(WasmValue[] args);

    /// <summary>
    /// sock_recv(fd: fd, ri_data: iovec_array, ri_flags: riflags) -> Result<(size, roflags), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] sock_recv(WasmValue[] args);

    /// <summary>
    /// sock_send(fd: fd, si_data: ciovec_array, si_flags: siflags) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] sock_send(WasmValue[] args);

    /// <summary>
    /// sock_shutdown(fd: fd, how: sdflags) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    WasmValue[] sock_shutdown(WasmValue[] args);

}
